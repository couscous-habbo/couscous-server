using System;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class ReceiveClientVersionPacket : IClientPacket
    {
        public void Handle(NetworkClient client, ClientPacketReader reader)
        {    
            Console.WriteLine("Client version: " + reader.ReadString());
        }
    }
}