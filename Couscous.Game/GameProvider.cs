using Couscous.Game.Players;

namespace Couscous.Game
{
    public class GameProvider
    {
        public PlayerProvider PlayerProvider;

        public GameProvider(PlayerProvider playerProvider)
        {
            PlayerProvider = playerProvider;
        }
    }
}