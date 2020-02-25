namespace Couscous.Networking.Packets.Client
{
    public struct ClientPacketId
    {
        public const int ReceiveClientVersionPacket = 4000;
        public const int RequestEncryptionKeysPacket = 1053;
        public const int ReceiveUniqueMachineIdPacket = 2490;
        public const int PerformanceLogPacket = 3230;
    }
}