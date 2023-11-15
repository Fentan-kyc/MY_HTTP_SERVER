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
            Console.WriteLine("Hello");

            TcpListener listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();

            var client = listener.AcceptTcpClient();
            using(var stream = client.GetStream())
            {
                using(var reader = new StreamReader(stream))
                {
                    while (true)
                    {
                        var result = reader.ReadLine();
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}
