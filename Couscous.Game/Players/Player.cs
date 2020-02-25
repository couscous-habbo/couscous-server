using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class Player : PlayerData, IDisposable
    {
        public Player(DataRow playerData) : base(playerData)
        {
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}