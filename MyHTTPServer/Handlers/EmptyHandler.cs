using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.Handlers
{
    public sealed class EmptyHandler : IHandler
    {
        public void Handle(Stream stream)
        {
            using(var reader = new StreamReader(stream))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (string line = null; line != string.Empty; line = reader.ReadLine())
                    {
                        Console.WriteLine(line);
                    }

                    writer.WriteLine("Empty");
                }
            }
        }
    }
}
