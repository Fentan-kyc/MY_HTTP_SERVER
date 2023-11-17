using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer
{
    internal static class ResponseWriter
    {
        public static void WriteStatus (HttpStatusCode code, Stream stream)
        {
            using var writer = new StreamWriter(stream, leaveOpen: true);
            writer.WriteLine($"HTTP/1.1 {(int)code} {code}");
            writer.WriteLine();
        }
    }
}
