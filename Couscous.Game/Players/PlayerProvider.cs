using System.Threading.Tasks;

namespace Couscous.Game.Players
{
    public class PlayerProvider
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerProvider(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> GetPlayerBySsoTicketAsync(string ssoTicket)
        {
            return await _playerRepository.GetPlayerBySsoTicketAsync(ssoTicket);
        }
    }
}