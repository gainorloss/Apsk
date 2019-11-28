using Infrastructure.AOP;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    public interface IFunctionTests
    {
        Task SayHelloAsync();
    }
}
