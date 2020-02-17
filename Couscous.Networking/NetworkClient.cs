using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Couscous.Networking
{
    public class NetworkClient : IDisposable
    {
        private readonly TcpClient _tcpClient;
        
        public NetworkClient(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public async void StartReceiving()
        {
            await ProcessDataAsync();
        }

        private async Task ProcessDataAsync()
        {
            while (true)
            {
                var binaryData = await GetBinaryDataAsync();
                Console.WriteLine(Encoding.Default.GetString(binaryData));
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

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}