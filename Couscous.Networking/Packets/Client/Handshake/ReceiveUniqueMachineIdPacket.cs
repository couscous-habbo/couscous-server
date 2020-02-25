using System;
using System.Threading.Tasks;
using Couscous.Networking.Packets.Server.Handshake;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class ReceivedUniqueMachineIdPacket : IClientPacket
    {
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {
            await client.WriteToStreamAsync(new SendUniqueMachineIdPacket(reader.ReadString()).GetBytes());
        }
    }
}