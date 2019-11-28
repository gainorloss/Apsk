using Infrastructure.ConsoleApp.Services;
using System;
using System.Reflection;

namespace Infrastructure.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //int? input = null;
                //input -= 10;
                DynamicProxyCaller();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        private static void DynamicProxyCaller()
        {
            var proxy = DispatchProxy.Create<IUsrService, ServiceProxy>();
            proxy.SayHello();
        }
    }
}
