using System;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class ReceiveClientVersionPacket : IClientPacket
    {
        public void Process(NetworkClient client, ClientPacketData packet)
        {    
            Console.WriteLine("ReceiveClientVersionPacket");
        }
    }
}