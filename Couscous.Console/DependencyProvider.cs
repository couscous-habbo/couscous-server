using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Couscous.Config;
using Couscous.Database;
using Couscous.Game;
using Couscous.Game.Players;
using Couscous.Logging;
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
        public void Load()
        {
            IConfigProvider configProvider = new RemoteJsonConfigProvider();
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

            this.AddSingleton<LogFactory>();
            this.AddTransient(provider => provider.GetService<LogFactory>().GetLogger());
            this.AddSingleton<PlayerHandler>();
            
            this.AddSingleton(provider => new Dictionary<int, IClientPacket> // todo: load these dynamically
            {
                { ClientPacketId.SendPolicyFileRequest, new SendPolicyFilePacket() },
                { ClientPacketId.ReceiveClientVersion, new ReceivedClientVersionPacket() },
                { ClientPacketId.RequestEncryptionKeys, new RequestEncryptionKeysPacket() },
                { ClientPacketId.ReceiveUniqueMachineId, new ReceivedUniqueMachineIdPacket() },
                { ClientPacketId.PerformanceLog, new PerformanceLogPacket() },
                { ClientPacketId.SecureLogin, new SecureLoginPacket(this.BuildServiceProvider().GetService<PlayerHandler>()) }
            });
            
            this.AddSingleton<IDatabaseProvider, DatabaseProvider>(provider => new DatabaseProvider(connectionString, provider.GetService<LogFactory>()));
            this.AddSingleton<PlayerDao>();
            this.AddSingleton<PlayerRepository>();
            this.AddSingleton<GameProvider>();
            this.AddSingleton<ClientPacketProvider>();
            this.AddSingleton<NetworkHandler>();
            this.AddSingleton(provider => new TcpListener(IPAddress.Any, int.Parse(configProvider.GetValueFromKey("networking.port"))));
            this.AddSingleton<NetworkListener>();
            this.AddSingleton<Server>();
        }
    }
}