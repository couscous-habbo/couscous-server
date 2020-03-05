namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SendUniqueMachineIdPacket : ServerPacketBuilder
    {
        public SendUniqueMachineIdPacket(string uniqueMachineId) : base(ServerPacketId.ReceivedUniqueMachineId)
        {
            WriteString(uniqueMachineId);
        }
    }
}