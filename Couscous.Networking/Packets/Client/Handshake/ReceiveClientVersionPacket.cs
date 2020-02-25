using System;
using System.Threading.Tasks;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class ReceivedClientVersionPacket : IClientPacket
    {
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {    
            Console.WriteLine("Client version: " + reader.ReadString());
        }
    }
}