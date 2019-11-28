using System.Linq;
using System.Reflection;

namespace Infrastructure.ConsoleApp.Services
{
    public class ServiceProxy
          : DispatchProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            System.Console.WriteLine("执行之前(无线电)");

            var implementations = BootstrapClassLoader.GetServiceType(targetMethod.DeclaringType);
            var returnValue = targetMethod.Invoke(implementations.FirstOrDefault(), args);

            System.Console.WriteLine("执行之后(无线电)");

            return returnValue;
        }
    }
}
