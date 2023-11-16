using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer
{
    public interface IHandler
    {
        void Handle(Stream stream);
    }
}
