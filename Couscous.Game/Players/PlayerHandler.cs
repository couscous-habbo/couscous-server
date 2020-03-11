using System;
using System.Threading.Tasks;

namespace Couscous.Game.Players
{
    public class PlayerHandler : IDisposable
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerHandler(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> GetPlayerBySsoTicketAsync(string ssoTicket)
        {
            return await _playerRepository.GetPlayerBySsoTicketAsync(ssoTicket);
        }
        
        public bool TryRegisterPlayer(Player player)
        {
            return _playerRepository.TryRegisterPlayer(player);
        }

        public void Dispose()
        {
            _playerRepository.Dispose();
        }
    }
}