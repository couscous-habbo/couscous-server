namespace Couscous.Networking.Packets.Server.Handshake
{
    public class UserHomeRoomPacket : ServerPacketBuilder
    {
        public UserHomeRoomPacket(int homeRoom, int unknown) : base(ServerPacketId.SendUserHomeRoom)
        {
            WriteInteger(homeRoom);
            WriteInteger(unknown);
        }
    }
}