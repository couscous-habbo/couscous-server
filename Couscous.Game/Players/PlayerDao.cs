using System;
using System.Threading.Tasks;
using Couscous.Database;

namespace Couscous.Game.Players
{
    public class PlayerDao : DatabaseAccessObject
    {
        private readonly PlayerFactory _playerFactory;

        public PlayerDao(IDatabaseProvider databaseProvider, PlayerFactory playerFactory) : base(databaseProvider)
        {
            _playerFactory = playerFactory;
        }

        public async Task<Player> GetPlayerBySsoTicketAsync(string ssoTicket)
        {
            Player player = null;
            
            using (var dbConnection = GetConnection())
            {
                dbConnection.SetQuery("SELECT users.id, user_data.home_room FROM users LEFT JOIN user_data ON users.id = user_data.user_id WHERE users.auth_ticket = @authTicket");
                dbConnection.AddParameter("authTicket", ssoTicket);

                var playerRow = await dbConnection.ExecuteRowAsync();

                if (playerRow != null)
                {
                    player = _playerFactory.GetPlayerFromDataRow(playerRow);
                }
            }
            
            return player;
        }
    }
}