﻿using System;
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
        public void Handle(Stream networkStream, Request request)
        {
                using (var writer = new StreamWriter(networkStream))
                {
                    var filePath = Path.Combine(_path, request.Path.Substring(1));

                    Console.WriteLine("\n" + $"Request: {request}");
                    Console.WriteLine(filePath);

                    if(!File.Exists(filePath))
                    {
                        ResponseWriter.WriteStatus(HttpStatusCode.NotFound, networkStream);
                    }
                    else
                    {
                        ResponseWriter.WriteStatus(HttpStatusCode.OK, networkStream);

                        using (var fileStream = File.OpenRead(filePath))
                        {
                            fileStream.CopyTo(networkStream);
                        }
                    }
                }
        }
    }
}
