using System.Net;
using System.Net.Sockets;
using Couscous.Console;
using Couscous.Database;
using Couscous.Game;
using Couscous.Game.Players;
using Couscous.Logging;
using Couscous.Networking;
using Couscous.Networking.Packets.Client;
using NUnit.Framework;

namespace Couscous.Tests
{
    [TestFixture]
    public class CompositionRootTest
    {
        [Test]
        public void Test()
        {
            var dp = new DependencyProvider();
            Assert.DoesNotThrow(() => dp.Load());
        }
    }
}