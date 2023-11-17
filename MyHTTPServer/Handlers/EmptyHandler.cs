using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.Handlers
{
    public sealed class EmptyHandler : IHandler
    {
        public void Handle(Stream stream, Request request)
        {
                using (var writer = new StreamWriter(stream))
                {
                ResponseWriter.WriteStatus(HttpStatusCode.OK, stream);
                    writer.WriteLine("Empty");
                }
        }
    }
}
