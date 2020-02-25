using Couscous.Networking;

namespace Couscous.Console
{
    public class Server
    {
        private readonly NetworkListener _networkListener;
        
        public Server(NetworkListener networkListener)
        {
            _networkListener = networkListener;
        }

        public void Start()
        {
            _networkListener.StartListener();
            _networkListener.ListenAsync();
        }
    }
}