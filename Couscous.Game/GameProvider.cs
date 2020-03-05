using System;
using Couscous.Game.Players;

namespace Couscous.Game
{
    public class GameProvider : IDisposable
    {
        public PlayerHandler PlayerHandler;

        public GameProvider(PlayerHandler playerHandler)
        {
            PlayerHandler = playerHandler;
        }

        public void Dispose()
        {
            PlayerHandler.Dispose();
        }
    }
}