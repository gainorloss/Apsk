// <copyright file="IServiceDiscoveryClient.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Abstractions
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IServiceDiscoveryClient
    {
        Task<HttpResponseMessage> SendAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
