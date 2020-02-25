using System.Collections.Generic;
using System.Threading.Tasks;

namespace Couscous.Game.Players
{
    public class PlayerRepository
    {
        private readonly IDictionary<int, Player> _players;
        private readonly PlayerDao _playerDao;
        
        public PlayerRepository(PlayerDao playerDao)
        {
            _players = new Dictionary<int, Player>();
            _playerDao = playerDao;
        }

        public async Task<Player> GetPlayerBySsoTicketAsync(string ssoTicket)
        {
            return await _playerDao.GetPlayerBySsoTicketAsync(ssoTicket);
        }

        public bool TryRegisterPlayer(Player player)
        {
            return _players.TryAdd(player.Id, player);
        }
    }
}