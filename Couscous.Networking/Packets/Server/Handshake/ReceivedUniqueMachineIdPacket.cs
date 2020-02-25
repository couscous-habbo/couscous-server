namespace Couscous.Networking.Packets.Server.Handshake
{
    public class ReceivedUniqueMachineIdPacket : ServerPacketBuilder
    {
        public ReceivedUniqueMachineIdPacket(string uniqueMachineId) : base(1488)
        {
            WriteString(uniqueMachineId);
        }
    }
}