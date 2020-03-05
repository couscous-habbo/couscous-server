namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SecureLoginPacket : ServerPacketBuilder
    {
        public SecureLoginPacket() : base(ServerPacketId.SecureLogin)
        {
        }
    }
}