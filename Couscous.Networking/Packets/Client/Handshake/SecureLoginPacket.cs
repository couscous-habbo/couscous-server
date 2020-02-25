using System.Threading.Tasks;
using Couscous.Game.Players;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class SecureLoginPacket : IClientPacket
    {
        private readonly PlayerProvider _playerProvider;
        
        public SecureLoginPacket(PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }
        
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {
            client.Player = await _playerProvider.GetPlayerBySsoTicketAsync(reader.ReadString());
        }
    }
}