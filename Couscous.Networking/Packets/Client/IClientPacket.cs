using System.Threading.Tasks;

namespace Couscous.Networking.Packets.Client
{
    public interface IClientPacket
    {
        Task HandleAsync(NetworkClient client, ClientPacketReader reader);
    }
}