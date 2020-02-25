using System.Threading.Tasks;
using Couscous.Database;

namespace Couscous.Game.Players
{
    public class PlayerDao : DatabaseAccessObject
    {
        public PlayerDao(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        public async Task<Player> GetPlayerBySsoTicketAsync(string ssoTicket)
        {
            Player player = null;
            
            using (var dbConnection = GetConnection())
            {
                dbConnection.SetQuery("SELECT `username` FROM `users` WHERE `auth_ticket` = @authTicket");
                dbConnection.AddParameter("authTicket", ssoTicket);

                var playerRow = await dbConnection.ExecuteRowAsync();

                if (playerRow != null)
                {
                    player = new Player(await dbConnection.ExecuteRowAsync());
                }
            }
            
            return player;
        }
    }
}