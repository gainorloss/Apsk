// <copyright file="IDiscoveryClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Apsk.AOP;

    public interface IDiscoveryClient
    {
        [HystrixCommand()]
        Task<HttpResponseMessage> SendAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
