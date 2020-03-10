using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Config;
using Couscous.Database;
using Couscous.Game;
using Couscous.Game.Players;
using Couscous.Networking;
using Couscous.Networking.Packets.Client;
using Couscous.Networking.Packets.Client.Handshake;
using Couscous.Networking.Packets.Client.Tracking;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Couscous.Console
{
    public class DependencyProvider : ServiceCollection, IServiceCollection
    {
        /// <summary>
        /// Register all of the dependencies to the container
        /// </summary>
        public void Register()
        {
            this.AddSingleton<IConfigProvider, RemoteJsonConfigProvider>();

            var serviceProvider = this.BuildServiceProvider();
            var configProvider = serviceProvider.GetService<IConfigProvider>();
            
            configProvider.Load("http://tiny.cc/zo6gkz"); // cba :(
            
            var connectionString = new MySqlConnectionStringBuilder
            {
                Database = configProvider.GetValueFromKey("database.name"),
                Password = configProvider.GetValueFromKey("database.password"),
                Port = uint.Parse(configProvider.GetValueFromKey("database.port")),
                Server = configProvider.GetValueFromKey("database.host"),
                UserID = configProvider.GetValueFromKey("database.username"),
                SslMode = MySqlSslMode.None
            }.ToString();

            this.AddSingleton<IDatabaseProvider, DatabaseProvider>(provider => new DatabaseProvider(connectionString));
            this.AddSingleton(provider => new PlayerDao(serviceProvider.GetService<IDatabaseProvider>()));
            
            var playerRepository = new PlayerRepository(serviceProvider.GetService<PlayerDao>());

            this.AddSingleton(provider => new PlayerHandler(playerRepository));
            this.AddSingleton<GameProvider>();
            
            var packets = new Dictionary<int, IClientPacket>
            {
                { ClientPacketId.SendPolicyFileRequest, new SendPolicyFilePacket() },
                { ClientPacketId.ReceiveClientVersion, new ReceivedClientVersionPacket() },
                { ClientPacketId.RequestEncryptionKeys, new RequestEncryptionKeysPacket() },
                { ClientPacketId.ReceiveUniqueMachineId, new ReceivedUniqueMachineIdPacket() },
                { ClientPacketId.PerformanceLog, new PerformanceLogPacket() },
                { ClientPacketId.SecureLogin, new SecureLoginPacket(serviceProvider.GetService<PlayerHandler>()) }
            };

            this.AddSingleton(provider => new ClientPacketProvider(packets));
            this.AddSingleton(provider => new NetworkHandler(new List<NetworkClient>(), serviceProvider.GetService<ClientPacketProvider>()));
            
            var networkListener = new NetworkListener(
                new TcpListener(IPAddress.Any, int.Parse(configProvider.GetValueFromKey("networking.port"))),
                serviceProvider.GetService<NetworkHandler>()
            );

            var server = new Server(networkListener, serviceProvider.GetService<GameProvider>());
            server.Start();
        }
    }
}