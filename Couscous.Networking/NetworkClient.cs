using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Couscous.Networking.Helpers;

namespace Couscous.Networking
{
    public class NetworkClient : IDisposable
    {
        private readonly TcpClient _tcpClient;
        
        public NetworkClient(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
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
                var messageLength = NetworkHelpers.DecodeInt(br.ReadBytes(4));
                var packetData = br.ReadBytes(messageLength);

                using var br2 = new BinaryReader(new MemoryStream(packetData));
                var packetId = NetworkHelpers.DecodeShort(br2.ReadBytes(2));

                if (packetId == 26979)
                {
                    await WriteToStreamAsync(Encoding.Default.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<policy-file-request/><allow-access-from domain=\"*\" to-ports=\"*\" />\r\n</cross-domain-policy>\0)"));
                }
                else
                {
                    Console.WriteLine(packetId);
                }
            }
        }
        
        private async Task<byte[]> GetBinaryDataAsync()
        {
            var buffer = new byte[2048];
            var memoryStream = new MemoryStream();
        
            var bytesRead = await _tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

            while (bytesRead > 0)
            {
                memoryStream.Write(buffer, 0, buffer.Length);
                bytesRead = await memoryStream.ReadAsync(buffer, 0, buffer.Length);
            }

            return memoryStream.ToArray();
        }

        private async Task WriteToStreamAsync(byte[] data)
        {
            await _tcpClient.GetStream().WriteAsync(data, 0, data.Length);
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}