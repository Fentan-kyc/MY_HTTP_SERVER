using MyHTTPServer;
using MyHTTPServer.Handlers;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //MyHTTPServerHost host = new MyHTTPServerHost(new EmptyHandler());
            //MyHTTPServerHost host = new MyHTTPServerHost(new StaticFileHandler(Path.Combine(Environment.CurrentDirectory, "www")));

            MyHTTPServerHost host = new MyHTTPServerHost(new ControllersHandler(typeof(Program).Assembly));
            await host.StartAsync();
        }
    }
}
