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
    public class DependencyProvider : ServiceCollection
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

            var packets = new Dictionary<int, IClientPacket>
            {
                { ClientPacketId.SendPolicyFileRequest, new SendPolicyFilePacket() },
                { ClientPacketId.ReceiveClientVersion, new ReceivedClientVersionPacket() },
                { ClientPacketId.RequestEncryptionKeys, new RequestEncryptionKeysPacket() },
                { ClientPacketId.ReceiveUniqueMachineId, new ReceivedUniqueMachineIdPacket() },
                { ClientPacketId.PerformanceLog, new PerformanceLogPacket() },
                { ClientPacketId.SecureLogin, new SecureLoginPacket(serviceProvider.GetService<PlayerHandler>()) }
            };
            
            this.AddSingleton<IDatabaseProvider, DatabaseProvider>(provider => new DatabaseProvider(connectionString));
            this.AddSingleton(provider => new PlayerDao(serviceProvider.GetService<IDatabaseProvider>()));
            this.AddSingleton<PlayerRepository>();
            this.AddSingleton<PlayerHandler>();
            this.AddSingleton<GameProvider>();
            this.AddSingleton(provider => new ClientPacketProvider(packets));
            this.AddSingleton(provider => new NetworkHandler(new List<NetworkClient>(), serviceProvider.GetService<ClientPacketProvider>()));
            this.AddSingleton(provider => new TcpListener(IPAddress.Any, int.Parse(configProvider.GetValueFromKey("networking.port"))));
            this.AddSingleton<NetworkListener>();
            this.AddSingleton<Server>();
        }

        public void Load()
        {
            var serviceProvider = this.BuildServiceProvider();
            
            serviceProvider.GetService<Server>().Start();
        }
    }
}