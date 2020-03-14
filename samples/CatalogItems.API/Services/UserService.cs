using Apsk.Annotations;
using Apsk.Cloud.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatalogItems.API.Services
{
    [Service]
    public class UserService
        : IUserService
    {
        private readonly IDiscoveryClient _discoveryClient;
        public UserService(IDiscoveryClient discoveryClient)
        {
            _discoveryClient = discoveryClient;
        }
        public async Task<string> GetNameAsync()
        {
            var ret = await _discoveryClient.SendAsync("UsersAPI", "api.user.getname/v1.0", HttpMethod.Get);
            return ret.Result.ToString();
        }
    }
}
