using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Database;
using Couscous.Game;
using Couscous.Game.Players;
using Couscous.Networking;
using Couscous.Networking.Packets.Client;
using Couscous.Networking.Packets.Client.Handshake;
using Couscous.Networking.Packets.Client.Tracking;
using MySql.Data.MySqlClient;

namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var connectionString = new MySqlConnectionStringBuilder
            {
                Database = "habbo",
                Password = "secret",
                Port = 3306,
                Server = "127.0.0.1",
                UserID = "root",
                SslMode = MySqlSslMode.None
            }.ToString();
            
            var databaseProvider = new DatabaseProvider(connectionString);
            
            var playerDao = new PlayerDao(databaseProvider);
            var playerRepository = new PlayerRepository(playerDao);
            var playerProvider = new PlayerProvider(playerRepository);
            var gameProvider = new GameProvider(playerProvider);
            
            var packets = new Dictionary<int, IClientPacket>
            {
                { ClientPacketId.ReceiveClientVersion, new ReceivedClientVersionPacket() },
                { ClientPacketId.RequestEncryptionKeys, new RequestEncryptionKeysPacket() },
                { ClientPacketId.ReceiveUniqueMachineId, new ReceivedUniqueMachineIdPacket() },
                { ClientPacketId.PerformanceLog, new PerformanceLogPacket() },
                { ClientPacketId.SecureLogin, new SecureLoginPacket(playerRepository) }
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