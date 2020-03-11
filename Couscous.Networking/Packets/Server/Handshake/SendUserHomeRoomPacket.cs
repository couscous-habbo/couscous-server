namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SendUserHomeRoomPacket : ServerPacketBuilder
    {
        public SendUserHomeRoomPacket(int homeRoom, int unknown) : base(ServerPacketId.SendUserHomeRoom)
        {
            WriteInteger(homeRoom);
            WriteInteger(unknown);
        }
    }
}