using System.Collections.Generic;

namespace Couscous.Networking.Packets.Client
{
    public class ClientPacketProvider
    {
        private readonly Dictionary<int, IClientPacket> _packets;

        public ClientPacketProvider(Dictionary<int, IClientPacket> packets)
        {
            _packets = packets;
        }

        public bool TryGetPacket(int packetId, out IClientPacket packet)
        {
            return _packets.TryGetValue(packetId, out packet);
        }
    }
}