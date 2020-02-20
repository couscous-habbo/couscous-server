namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SendEncryptionKeysPacket : ServerPacketBuilder
    {
        public SendEncryptionKeysPacket() : base(3531)
        {
            WriteString("");
            WriteString("");
        }
    }
}