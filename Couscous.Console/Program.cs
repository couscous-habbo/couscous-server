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
            System.Console.WriteLine("Hello World!");

            var packets = new Dictionary<int, IClientPacket>
            {
                { 4000, new ReceiveClientVersionPacket( )}
            };

            var packetProvider = new ClientPacketProvider(packets);
            
            var networkHandler = new NetworkHandler(
                new TcpListener(IPAddress.Any, 1232),
                new List<NetworkClient>(),
                packetProvider
            );

            networkHandler.StartListener();
            networkHandler.ListenAsync();

            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
    }
}