using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class PlayerData
    {
        public readonly int Id;
        public readonly string Username;
        
        public PlayerData(DataRow playerData)
        {
            Id = Convert.ToInt32(playerData["id"]);
            Username = Convert.ToString(playerData["username"]);
        }
    }
}