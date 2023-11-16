using MyHTTPServer.Handlers;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MyHTTPServer
{
    public sealed class MyHTTPServerHost
    {
        private readonly int _port = 80;
        private readonly IHandler _handler;

        public MyHTTPServerHost()
        {
            _handler = new EmptyHandler();
        }

        public MyHTTPServerHost(int port)
        {
            _port = port;
        }

        public MyHTTPServerHost(IHandler handler, int port = 80)
        {
            _handler = handler;
            _port = port;
        }

        public void Start()
        {
            Console.WriteLine("Starting Server...");
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            Console.WriteLine("done.");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    _handler.Handle(stream);
                }
            }
        }
    }
}
