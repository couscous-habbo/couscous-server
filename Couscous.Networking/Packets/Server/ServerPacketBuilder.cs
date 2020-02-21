using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Couscous.Networking.Packets.Server
{
    public class ServerPacketBuilder
    {
        private readonly ArrayBufferWriter<byte> _packet;
        
        protected ServerPacketBuilder(short packetId)
        {
            _packet = new ArrayBufferWriter<byte>();
            
            WriteShort(packetId);
        }

        protected void WriteString(string data)
        {
            WriteShort((short) data.Length);
            WriteBytes(Encoding.Default.GetBytes(data));
        }

        private void WriteBytes(byte[] data)
        {
            _packet.Write(data);
        }

        private void WriteShort(short length)
        {
            _packet.Write(BitConverter.GetBytes(length));
        }

        public byte[] GetBytes()
        {
            var finalBytes = new List<byte>();
            
            finalBytes.AddRange(BitConverter.GetBytes(_packet.WrittenCount));
            finalBytes.Reverse();
            finalBytes.AddRange(_packet.WrittenSpan.ToArray());

            return finalBytes.ToArray();
        }
    }
}