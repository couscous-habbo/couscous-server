using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Networking;

namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var networkHandler = new NetworkHandler(
                new TcpListener(IPAddress.Any, 1232),
                new List<NetworkClient>()
            );
            
            networkHandler.ListenAsync();

            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
    }
}