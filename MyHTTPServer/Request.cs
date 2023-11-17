using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer
{
    public record Request
    {
        public string Path;
        public HttpMethod Method;

        public Request(string path, HttpMethod method)
        {
            Path = path;
            Method = method;
        }

        public override string ToString()
        {
            return $"Path: {Path}, Method: {Method}";
        }
    }
}
