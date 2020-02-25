using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Networking;
using Couscous.Networking.Packets.Client;
using Couscous.Networking.Packets.Client.Handshake;
using Couscous.Networking.Packets.Client.Tracking;

namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var packets = new Dictionary<int, IClientPacket>
            {
                { ClientPacketId.ReceiveClientVersionPacket, new ReceiveClientVersionPacket() },
                { ClientPacketId.RequestEncryptionKeysPacket, new RequestEncryptionKeysPacket() },
                { ClientPacketId.ReceiveUniqueMachineIdPacket, new ReceiveUniqueMachineIdPacket() },
                { ClientPacketId.PerformanceLogPacket, new PerformanceLogPacket() }
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