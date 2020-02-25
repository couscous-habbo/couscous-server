using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class PlayerData
    {
        public string Username;
        
        public PlayerData(DataRow playerData)
        {
            Username = Convert.ToString(playerData["username"]);
        }
    }
}