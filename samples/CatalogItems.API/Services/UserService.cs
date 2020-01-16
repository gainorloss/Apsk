using Apsk.Annotations;
using Apsk.Cloud.Abstractions;
using DnsClient;
using System;
using System.Linq;
using System.Net.Http;
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
            if (entry == null)
                throw new Exception();

            var ip = $"http://{entry.HostName.Substring(0, entry.HostName.Length - 1)}:{entry.Port}";
            var name = await _httpClient.SendAsync<string>($"{ip}/api.user.getname/v1.0", HttpMethod.Get);
            return await name.Content.ReadAsStringAsync();
        }
    }
}
