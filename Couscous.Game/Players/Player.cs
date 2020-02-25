using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class Player : PlayerData
    {
        public Player(DataRow playerData) : base(playerData)
        {
        }
    }
}