using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;


namespace MyHTTPServer.Handlers
{
    public class ControllersHandler : IHandler
    {
        private readonly Dictionary<string, Func<object>> _routes;

        public ControllersHandler(Assembly controllersAssembly)
        {
            _routes = controllersAssembly.GetTypes().Where(x => typeof(IController).IsAssignableFrom(x))
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
                return "/" + name;
            else
                return "/" + name + "/" + method.Name;
        }

        public void Handle(Stream networkStream, Request request)
        {
            if (!_routes.TryGetValue(request.Path, out var func)) ResponseWriter.WriteStatus(HttpStatusCode.NotFound, networkStream);
            else
            {
                ResponseWriter.WriteStatus(HttpStatusCode.OK, networkStream);
                WriteControllerResponce(func(), networkStream);
            }
        }

        private void WriteControllerResponce(object response, Stream networkStream)
        {
            switch (response)
            {
                case String:
                    using (var writer = new StreamWriter(networkStream))
                    {
                        writer.Write((String)response);
                    }
                        break;

                case Byte[]:
                    byte[] resByte = (byte[])response;
                    networkStream.Write(resByte, 0, resByte.Length);
                    break;

                default:
                    WriteControllerResponce(JsonConvert.SerializeObject(response), networkStream);
                    break;
            }
        }
    }
}
