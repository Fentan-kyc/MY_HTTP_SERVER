using MyHTTPServer.Handlers;
using MyHTTPServer.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
                using (var client = listener.AcceptTcpClient())
                {
                    using (var networkStream = client.GetStream())
                    {
                        List<string> buffer = new List<string>();

                        using (var reader = new StreamReader(networkStream))
                        {
                            for (string line = null; line != string.Empty; line = reader.ReadLine())
                            {
                                if (!string.IsNullOrEmpty(line)) buffer.Add(line);
                                Console.WriteLine(line);
                            }

                            var requst = RequestParser.Parse(buffer);

                            _handler.Handle(networkStream, requst);
                        }
                    }
                }
            }
        }

        public void StartAsync()
        {
            Console.WriteLine("Starting Server...");
            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            Console.WriteLine("done.");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ProcessClient(client);
            }
        }

        private void ProcessClient(TcpClient client)
        {
            ThreadPool.QueueUserWorkItem(o => {
                using (client)
                {
                    try
                    {
                        using (var networkStream = client.GetStream())
                        {
                            List<string> buffer = new List<string>();

                            using (var reader = new StreamReader(networkStream))
                            {
                                for (string line = null; line != string.Empty; line = reader.ReadLine())
                                {
                                    if (!string.IsNullOrEmpty(line)) buffer.Add(line);
                                    Console.WriteLine(line);
                                }

                                var requst = RequestParser.Parse(buffer);

                                _handler.Handle(networkStream, requst);
                            }
                        }
                    }
                    catch(IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            });
            
        }
    }
}
