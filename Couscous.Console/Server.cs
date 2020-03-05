using System;
using Couscous.Game;
using Couscous.Networking;

namespace Couscous.Console
{
    public class Server : IDisposable
    {
        private readonly NetworkListener _networkListener;
        private readonly GameProvider _gameProvider;
        
        public Server(NetworkListener networkListener, GameProvider gameProvider)
        {
            _networkListener = networkListener;
            _gameProvider = gameProvider;
        }

        public void Start()
        {
            _networkListener.StartListener();
            _networkListener.ListenAsync();
        }

        public void Dispose()
        {
            _networkListener.Dispose();
            _gameProvider.Dispose();
        }
    }
}