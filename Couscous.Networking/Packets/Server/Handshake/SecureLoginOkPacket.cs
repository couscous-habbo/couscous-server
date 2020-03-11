namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SecureLoginOkPacket : ServerPacketBuilder
    {
        public SecureLoginOkPacket() : base(ServerPacketId.SecureLoginOk)
        {
        }
    }
}