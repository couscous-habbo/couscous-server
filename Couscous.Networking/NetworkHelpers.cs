namespace Couscous.Networking.Helpers
{
    public static class NetworkHelpers
    {
        public static int DecodeInt(byte[] data)
        {
            if ((data[0] | data[1] | data[2] | data[3]) < 0)
            {
                return -1;
            }
            
            return (data[0] << 24) + (data[1] << 16) + (data[2] << 8) + data[3];
        }
        
        public static int DecodeShort(byte[] data)
        {
            if ((data[0] | data[1]) < 0)
            {
                return -1;
            }
            
            return (data[0] << 8) + data[1];
        }
    }
}