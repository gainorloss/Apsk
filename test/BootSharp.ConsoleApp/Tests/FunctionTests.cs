using Infrastructure.Annotations;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    [Component]
    public class FunctionTests
         : IFunctionTests
    {
        public Task SayHelloAsync()
        {
            return Task.CompletedTask;
        }
    }
}
