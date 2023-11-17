using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using MyHTTPServer.Parsers;
using System.Net;

namespace MyHTTPServer.Handlers
{
    public sealed class StaticFileHandler : IHandler
    {
        private readonly string _path;
        public StaticFileHandler(string path)
        {
            _path = path;
        }
        public void Handle(Stream neteworkStream)
        {
            List<string> buffer = new List<string>();

            using (var reader = new StreamReader(neteworkStream))
            {
                using (var writer = new StreamWriter(neteworkStream))
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

                    if(!File.Exists(filePath))
                    {
                        //TODO 404 status
                        ResponseWriter.WriteStatus(HttpStatusCode.NotFound, neteworkStream);
                    }
                    else
                    {
                        ResponseWriter.WriteStatus(HttpStatusCode.OK, neteworkStream);

                        using (var fileStream = File.OpenRead(filePath))
                        {
                            fileStream.CopyTo(neteworkStream);
                        }
                    }
                }
            }
        }
    }
}
