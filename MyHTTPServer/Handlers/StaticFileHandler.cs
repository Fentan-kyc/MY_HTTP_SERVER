using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using MyHTTPServer.Parsers;

namespace MyHTTPServer.Handlers
{
    public sealed class StaticFileHandler : IHandler
    {
        private readonly string _path;
        public StaticFileHandler(string path)
        {
            _path = path;
        }
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

                    var request = RequestParser.Parse(buffer);

                    var filePath = Path.Combine(_path, request.Path.Substring(1));

                    Console.WriteLine("\n" + request);
                    Console.WriteLine(filePath);

                    writer.WriteLine("Static File");
                }
            }
        }
    }
}
