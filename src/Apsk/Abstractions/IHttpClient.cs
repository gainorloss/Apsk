using System.Net.Http;
using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync<T>(string url, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
