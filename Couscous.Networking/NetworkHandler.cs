using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Couscous.Networking.Packets.Client;

namespace Couscous.Networking
{
    public class NetworkHandler : IDisposable
    {
        private readonly IList<NetworkClient> _clients;
        private readonly ClientPacketProvider _packetProvider;

        public NetworkHandler(IList<NetworkClient> clients, ClientPacketProvider packetProvider)
        {
            _clients = clients;
            _packetProvider = packetProvider;
        }

        public void RegisterClient(TcpClient client)
        {
            var networkClient = new NetworkClient(client, _packetProvider);

            _clients.Add(networkClient);

            networkClient.StartReceiving();
        }

        public void Dispose()
        {
            foreach (var client in _clients)
            {
                client.Dispose();
            }
        }
    }
}