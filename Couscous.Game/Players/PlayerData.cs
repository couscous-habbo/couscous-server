using System;
using System.Data;

namespace Couscous.Game.Players
{
    public class PlayerData : IDisposable
    {
        public readonly int Id;
        public readonly string Username;
        public readonly int HomeRoom;

        protected PlayerData(int id, string username, int homeRoom)
        {
            Id = id;
            Username = username;
            HomeRoom = homeRoom;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}