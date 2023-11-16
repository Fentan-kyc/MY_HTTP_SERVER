using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.Parsers
{
    internal static class RequestParser
    {
        public static Request Parse(IEnumerable<string> buffer)
        {
            var first_string = buffer.First().Split(" ");
            var route = first_string[1];
            var method = first_string[0];

            return new Request(route, GetMethod(method));
        }

        private static HttpMethod GetMethod(string name)
        {
            if (name == "GET") return HttpMethod.Get;
            return HttpMethod.Post;
        }
    }
}
