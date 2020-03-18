using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class Player : PlayerData, IDisposable
    {
        public Player(int id, string username, int homeRoom) : base(id, username, homeRoom)
        {
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}