using System.Threading.Tasks;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class RequestEncryptionKeysPacket : IClientPacket
    {
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {
        }
    }
}