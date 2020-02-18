using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Couscous.Networking.Packets.Client;

namespace Couscous.Networking
{
    public class NetworkHandler : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly IList<NetworkClient> _clients;
        private readonly ClientPacketHandler _packetHandler;

        public NetworkHandler(TcpListener listener, IList<NetworkClient> clients, ClientPacketHandler packetHandler)
        {
            _listener = listener;
            _clients = clients;
            _packetHandler = packetHandler;
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
                var networkClient = new NetworkClient(tcpClient, _packetHandler);
                
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