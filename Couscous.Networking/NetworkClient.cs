using System;
using System.Net.Sockets;

namespace Couscous.Networking
{
    public class NetworkClient : IDisposable
    {
        private readonly TcpClient _client;
        
        public NetworkClient(TcpClient client)
        {
            _client = client;
        }

        public void StartReceiving()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}