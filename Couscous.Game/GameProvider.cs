using System;
using Couscous.Game.Players;

namespace Couscous.Game
{
    public class GameProvider : IDisposable
    {
        private readonly PlayerHandler _playerHandler;

        public GameProvider(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
        }

        public void Dispose()
        {
            _playerHandler.Dispose();
        }
    }
}