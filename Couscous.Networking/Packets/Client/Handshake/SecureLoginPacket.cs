using System.Threading.Tasks;
using Couscous.Game.Players;
using Couscous.Networking.Packets.Server.Handshake;

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
                client.Dispose();
                return;
            }

            client.Player = await _playerHandler.GetPlayerBySsoTicketAsync(sso);

            if (client.Player == null)
            {
                client.Dispose();
                return;
            }

            if (!_playerHandler.TryRegisterPlayer(client.Player))
            {
                client.Dispose();
                return;
            }
            
            // TODO: Check if the user is banned

            await client.WriteToStreamAsync(new SecureLoginOkPacket().GetBytes());
            await client.WriteToStreamAsync(new SendUserHomeRoomPacket(0, 0).GetBytes());
        }
    }
}