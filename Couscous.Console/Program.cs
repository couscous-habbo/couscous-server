using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Networking;
using Couscous.Networking.Packets.Client;
using Couscous.Networking.Packets.Client.Handshake;

namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var packets = new Dictionary<int, IClientPacket>
            {
                { ClientPacketIds.ReceiveClientVersionPacket, new ReceiveClientVersionPacket() },
                { ClientPacketIds.RequestEncryptionKeysPacket, new RequestEncryptionKeysPacket() }
            };

            var packetProvider = new ClientPacketProvider(packets);
            
            var networkHandler = new NetworkListener(
                new TcpListener(IPAddress.Any, 1232),
                new List<NetworkClient>(),
                packetProvider
            );

            var server = new Server(networkHandler);
            server.Start();

            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
    }
}