using Apsk.Abstractions;
using Apsk.Annotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatalogItems.API.Services
{
    [Service]
    public class UserService
        : IUserService
    {
        private readonly IServiceDiscoveryClient _serviceDiscoveryClient;
        public UserService(IServiceDiscoveryClient serviceDiscoveryClient)
        {
            _serviceDiscoveryClient = serviceDiscoveryClient;
        }
        public async Task<string> GetNameAsync()
        {
            var rsp = await _serviceDiscoveryClient.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
            return await rsp.Content.ReadAsStringAsync();
        }
    }
}
