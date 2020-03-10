using System;
using Couscous.Game;
using Couscous.Logging;
using Couscous.Networking;

namespace Couscous.Console
{
    public class Server : IDisposable
    {
        private readonly ILogger _logger;
        private readonly NetworkListener _networkListener;
        private readonly GameProvider _gameProvider;
        
        public Server(ILogger logger, NetworkListener networkListener, GameProvider gameProvider)
        {
            _logger = logger;
            _networkListener = networkListener;
            _gameProvider = gameProvider;
        }

        public void Start()
        {
            _networkListener.StartListener();
            _networkListener.ListenAsync();
            
            _logger.Success("Server has finished starting.");
        }

        public void Dispose()
        {
            _networkListener.Dispose();
            _gameProvider.Dispose();
        }
    }
}