using Couscous.Networking;

namespace Couscous.Console
{
    public class Server
    {
        private readonly NetworkHandler _networkHandler;
        
        public Server(NetworkHandler networkHandler)
        {
            _networkHandler = networkHandler;
        }

        public void Start()
        {
            _networkHandler.StartListener();
            _networkHandler.ListenAsync();
        }
    }
}