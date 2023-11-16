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

            while(true)
            {
            var client = listener.AcceptTcpClient();
            using(var stream = client.GetStream())
            {
                using(var reader = new StreamReader(stream))
                using(var writer = new StreamWriter(stream))
                {
                    for(string line = null; line != string.Empty; line = reader.ReadLine())
                    {
                        var result = reader.ReadLine();
                        Console.WriteLine(result);
                    }

                    writer.WriteLine("Hi, It's the simplest server in the hood.");
                }
            }
            }
        }
    }
}
