using System;
using System.Threading;
using Couscous.Database;
using Couscous.Game;
using Couscous.Logging;
using Couscous.Networking;

namespace Couscous.Console
{
    public class Server : IDisposable
    {
        private readonly ILogger _logger;
        private readonly IDatabaseProvider _databaseProvider;
        private readonly NetworkListener _networkListener;
        private readonly GameProvider _gameProvider;
        
        public bool Started { get; set; }
        
        public Server(
            ILogger logger, 
            IDatabaseProvider databaseProvider, 
            NetworkListener networkListener, 
            GameProvider gameProvider)
        {
            _logger = logger;
            _databaseProvider = databaseProvider;
            _networkListener = networkListener;
            _gameProvider = gameProvider;
        }

        public void Start()
        {
            if (_databaseProvider.IsConnected())
            {
                _logger.Error("Failed to connect to the database, press any key to exit.");
                ExitAfterKey();
            }
            
            _networkListener.StartListener();
            _networkListener.ListenAsync();
            
            _logger.Success("Server has finished starting.");

            Started = true;
            
            while (true)
            {
                System.Console.ReadLine(); 
                Thread.Sleep(100);
            }
        }

        private void ExitAfterKey()
        {
            System.Console.ReadKey(true);
            Dispose();
            
            Environment.Exit(-1);
        }

        public void Dispose()
        {
            _logger.Warning("Please wait, the server is disposing...");
            
            _networkListener.Dispose();
            _gameProvider.Dispose();
        }
    }
}