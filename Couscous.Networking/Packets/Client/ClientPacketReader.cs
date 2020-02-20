using System.Buffers.Binary;
using System.Text;

namespace Couscous.Networking.Packets.Client
{
    public class ClientPacketReader
    {
        private readonly byte[] _packetData;
        private int _packetPosition;
        
        public ClientPacketReader(byte[] packetData)
        {
            _packetData = packetData ?? new byte[0];
        }

        public string ReadString()
        {
            int packetLength = BinaryPrimitives.ReadInt16BigEndian(ReadBytes(2));
            return Encoding.Default.GetString(ReadBytes(packetLength));
        }

        private byte[] ReadBytes(int bytes)
        {
            if (bytes > _packetData.Length - _packetPosition)
            {
                bytes = _packetData.Length - _packetPosition;
            }
            
            var data = new byte[bytes];

            for (var i = 0; i < bytes; i++)
            {
                data[i] = _packetData[_packetPosition++];
            }

            return data;
        }
    }
}