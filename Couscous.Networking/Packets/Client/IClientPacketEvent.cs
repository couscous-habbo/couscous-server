namespace Couscous.Networking.Packets.Client
{
    public interface IClientPacket
    {
        void Process(NetworkClient client, ClientPacketData packet);
    }
}