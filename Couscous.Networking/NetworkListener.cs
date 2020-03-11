using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Couscous.Networking
{
    public class NetworkListener : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly NetworkHandler _handler;

        public NetworkListener(TcpListener listener, NetworkHandler handler)
        {
            _listener = listener;
            _handler = handler;
        }

        public void StartListener()
        {
            _listener.Start();
        }

        public async Task ListenAsync()
        {
            while (true)
            {
                _handler.RegisterClient(await _listener.AcceptTcpClientAsync());
            }
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}