using Apsk.Annotations;
using Apsk.Cloud.Abstractions;
using DnsClient;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogItems.API.Services
{
    [Service]
    public class UserService
        : IUserService
    {
        private readonly IDnsQuery _dnsQuery;
        private readonly IHttpClient _httpClient;
        public UserService(IDnsQuery dnsQuery,
            IHttpClient httpClient)
        {
            _dnsQuery = dnsQuery;
            _httpClient = httpClient;
        }
        public async Task<string> GetNameAsync()
        {
            var entry = _dnsQuery.ResolveService("service.consul", "UsersAPI").FirstOrDefault();
            var name = await _httpClient.GetStringAsync($"{entry.HostName.Substring(0, entry.HostName.Length-1)}/api.user.getname/v1.0");
            return name;
        }
    }
}
