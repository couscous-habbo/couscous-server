using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class PlayerFactory
    {
        public Player GetPlayerFromDataRow(DataRow row)
        {
            return new Player(
                Convert.ToInt32(row["id"]),
                Convert.ToString(row["username"]),
                Convert.ToInt32(row["home_room"])
            );
        }
    }
}