using Infrastructure.AOP;
using System;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    public interface IIntegrationTests
    {
        Task TestAccountLoginAsync();
        Task TestWeatherForecastLoginAsync();
    }
}
