using MyHTTPServer;
using MyHTTPServer.Handlers;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MyHTTPServerHost host = new MyHTTPServerHost(new StaticFileHandler(Path.Combine(Environment.CurrentDirectory, "www")));
            host.Start();
        }
    }
}
