using System.Text;
using System.Threading.Tasks;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class SendPolicyFilePacket : IClientPacket
    {
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {    
            await client.WriteToStreamAsync(Encoding.Default.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<policy-file-request/><allow-access-from domain=\"*\" to-ports=\"*\" />\r\n</cross-domain-policy>\0)"));
        }
    }
}