using System;
using System.Buffers.Binary;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Couscous.Networking.Packets.Client;

namespace Couscous.Networking
{
    public class NetworkClient
    {
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

                if (packetId == 26979)
                {
                    await WriteToStreamAsync(Encoding.Default.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<policy-file-request/><allow-access-from domain=\"*\" to-ports=\"*\" />\r\n</cross-domain-policy>\0)"));
                }
                else
                {
                    if (!_packetProvider.TryGetPacket(packetId, out var packet))
                    {
                        Console.WriteLine("Unhandled packet: " + packetId);
                        return;
                    }

                    var dataAfterLength = new byte[packetData.Length - 2];
                    Buffer.BlockCopy(packetData, 2, dataAfterLength, 0, packetData.Length - 2);

                    packet.Handle(this, new ClientPacketReader(dataAfterLength));
                }
            }
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

        private async Task WriteToStreamAsync(byte[] data)
        {
            await _networkStream.WriteAsync(data, 0, data.Length);
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}