using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class PlayerData : IDisposable
    {
        public readonly int Id;
        public readonly string Username;

        protected PlayerData(DataRow playerData)
        {
            Id = Convert.ToInt32(playerData["id"]);
            Username = Convert.ToString(playerData["username"]);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}