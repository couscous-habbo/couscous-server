using Couscous.Game.Players;

namespace Couscous.Game
{
    public class GameProvider
    {
        public PlayerHandler PlayerHandler;

        public GameProvider(PlayerHandler playerHandler)
        {
            PlayerHandler = playerHandler;
        }
    }
}