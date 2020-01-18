// <copyright file="IDiscoveryClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Cloud.Abstractions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Apsk.AOP;
    using Apsk.AspNetCore;

    public interface IDiscoveryClient
    {
        [HystrixCommand(nameof(FallbackAsync))]
        Task<RestResult> SendAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<RestResult> FallbackAsync(string service, string api, HttpMethod method, object data = null, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
