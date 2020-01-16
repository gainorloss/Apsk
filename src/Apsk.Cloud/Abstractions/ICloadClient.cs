using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apsk.Cloud.Abstractions
{
    public interface ICloadClient
    {
        Task<HttpResponseMessage> PostAsync<T>(string srv, string api, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
        Task<HttpResponseMessage> PostAsync(string srv, string api, Dictionary<string, string> dic, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
        Task<string> GetStringAsync(string srv, string api, string authorizationToken = null, string authorizationMethod = "Bearer");
        Task<HttpResponseMessage> DeleteAsync(string srv, string api, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
        Task<HttpResponseMessage> PutAsync<T>(string srv, string api, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
