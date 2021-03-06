using System;
using System.Buffers.Binary;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Couscous.Game.Players;
using Couscous.Networking.Packets.Client;

namespace Couscous.Networking
{
    public class NetworkClient
    {
        public Player Player;
        
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _networkStream;
        private readonly ClientPacketProvider _packetProvider;
        
        public NetworkClient(TcpClient tcpClient, ClientPacketProvider packetProvider)
        {
            _tcpClient = tcpClient;
            _networkStream = tcpClient.GetStream();
            _packetProvider = packetProvider;
        }

        public void StartReceiving()
        {
            Task.Run(ProcessDataAsync);
        }

        private async Task ProcessDataAsync()
        {
            while (true)
            {
                using var br = new BinaryReader(new MemoryStream(await GetBinaryDataAsync()));
                var messageLength = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
                var packetData = br.ReadBytes(messageLength);

                using var br2 = new BinaryReader(new MemoryStream(packetData));
                var packetId = BinaryPrimitives.ReadInt16BigEndian(br2.ReadBytes(2));

                await HandleIncomingPacket(packetData, packetId);
            }
        }

        private async Task HandleIncomingPacket(byte[] packetData, int packetId)
        {
            if (!_packetProvider.TryGetPacket(packetId, out var packet))
            {
                Console.WriteLine("Unhandled packet: " + packetId);
                return;
            }

            var dataAfterLength = new byte[packetData.Length - 2];
            Buffer.BlockCopy(packetData, 2, dataAfterLength, 0, packetData.Length - 2);

            await packet.HandleAsync(this, new ClientPacketReader(dataAfterLength));
        }
        
        private async Task<byte[]> GetBinaryDataAsync()
        {
            var buffer = new byte[2048];
            var memoryStream = new MemoryStream();
        
            var bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length);

            while (bytesRead > 0)
            {
                memoryStream.Write(buffer, 0, buffer.Length);
                bytesRead = await memoryStream.ReadAsync(buffer, 0, buffer.Length);
            }

            return memoryStream.ToArray();
        }

        public async Task WriteToStreamAsync(byte[] data)
        {
            await _networkStream.WriteAsync(data, 0, data.Length);
        }

        public void Dispose()
        {
            Player?.Dispose();
            
            _tcpClient.Dispose();
        }
    }
}