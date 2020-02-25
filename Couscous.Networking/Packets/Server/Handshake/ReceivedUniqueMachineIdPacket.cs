namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SendUniqueMachineIdPacket : ServerPacketBuilder
    {
        public SendUniqueMachineIdPacket(string uniqueMachineId) : base(1488)
        {
            WriteString(uniqueMachineId);
        }
    }
}