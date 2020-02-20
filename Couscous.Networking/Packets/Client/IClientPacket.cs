namespace Couscous.Networking.Packets.Client
{
    public interface IClientPacket
    {
        void Handle(NetworkClient client, ClientPacketReader reader);
    }
}