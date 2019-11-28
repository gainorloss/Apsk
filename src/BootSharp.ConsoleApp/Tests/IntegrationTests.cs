using Infrastructure.Annotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp
{
    [Component]
    public class IntegrationTests : IIntegrationTests
    {
        public async Task TestAccountLoginAsync()
        {
            await new HttpClient().GetAsync("http://localhost:5000/api/account/login");
        }
        public async Task TestWeatherForecastLoginAsync()
        {
            await new HttpClient().GetAsync("http://localhost:5000/api/weatherforecast/login");
        }
    }
}
