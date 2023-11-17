using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPServer.Handlers
{
    public class ControllersHandler : IHandler
    {
        private readonly Assembly _controllersAssembly;

        public ControllersHandler(Assembly controllersAssembly)
        {
            _controllersAssembly = controllersAssembly;
            _controllersAssembly.GetTypes().Where(x => typeof(IController).IsAssignableFrom(x))
                .SelectMany(Controller => Controller.GetMethods().Select(Method => new
                {
                    Controller,
                    Method
                })).ToDictionary(key => GetPath(key.Controller, key.Method), value => GetEndpointMethod(value.Controller, value.Method) );
        }

        private Func<Object> GetEndpointMethod(Type controller, MethodInfo method)
        {
            return () => method.Invoke(Activator.CreateInstance(controller), Array.Empty<Object>());
        }

        private String GetPath(Type controller, MethodInfo method)
        {
            string name = controller.Name;
            if (!name.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentException();

            name = name.Substring(0, name.Length - "controller".Length);

            if (method.Name.Equals("Index", StringComparison.InvariantCultureIgnoreCase))
                return name;
            else
                return "/" + name + "/" + method.Name;
        }

        public void Handle(Stream stream)
        {
            //Todo
        }
    }
}
