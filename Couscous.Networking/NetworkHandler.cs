using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Couscous.Networking
{
    public class NetworkHandler : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly IList<NetworkClient> _clients;

        public NetworkHandler(TcpListener listener, IList<NetworkClient> clients)
        {
            _listener = listener;
            _clients = clients;
        }

        public void StartListener()
        {
            _listener.Start();
        }

        public async Task ListenAsync()
        {
            while (true)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync();
                var networkClient = new NetworkClient(tcpClient);
                
                _clients.Add(networkClient);

                networkClient.StartReceiving();
            }
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}