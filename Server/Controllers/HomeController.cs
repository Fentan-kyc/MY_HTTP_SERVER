using MyHTTPServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class HomeController : IController
    {
        public record User(string Name, decimal Price);

        public User[] Index()
        {
            return new User[]
            {
                new User("Dmitrii", 40000m),
                new User("Nikolay", 23000m)
            };
        }
    }
}
