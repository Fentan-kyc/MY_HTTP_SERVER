using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace MyHTTPServer.Handlers
{
    public sealed class StaticFileHandler : IHandler
    {
        public void Handle(Stream stream)
        {
            List<string> buffer = new List<string>();

            using (var reader = new StreamReader(stream))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (string line = null; line != string.Empty; line = reader.ReadLine())
                    {
                        if (!string.IsNullOrEmpty(line)) buffer.Add(line);
                        Console.WriteLine(line);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Total fields: {buffer.Count}");
                    Console.ForegroundColor = ConsoleColor.White;

                    writer.WriteLine("Static File");
                }
            }
        }
    }
}
