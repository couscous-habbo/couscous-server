using System;
using Peach.Communication.Encryption;

namespace Couscous.Networking.Packets.Server.Handshake
{
    public class SendEncryptionKeysPacket : ServerPacketBuilder
    {
        public SendEncryptionKeysPacket() : base(0)
        {
            throw new NotImplementedException();
        }
    }
}