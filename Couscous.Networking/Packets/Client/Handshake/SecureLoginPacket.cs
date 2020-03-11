using System.Threading.Tasks;
using Couscous.Game.Players;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class SecureLoginPacket : IClientPacket
    {
        private readonly PlayerHandler _playerHandler;
        
        public SecureLoginPacket(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
        }
        
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {
            var sso = reader.ReadString();

            if (string.IsNullOrEmpty(sso)) 
            {
                return;
            }

            client.Player = await _playerHandler.GetPlayerBySsoTicketAsync(sso);

            if (client.Player == null)
            {
                return;
            }

            if (!_playerHandler.TryRegisterPlayer(client.Player))
            {
                client.Dispose();
            }
            
            // TODO: Check if the user is banned

            await client.WriteToStreamAsync(new Server.Handshake.SecureLoginPacket().GetBytes());
        }
    }
}