// <copyright file="ClientHostedService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Orleans;

    public class ClientHostedService
        : IHostedService
    {
        public readonly IClusterClient ClusterClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHostedService"/> class.
        /// </summary>
        public ClientHostedService()
        {
            ClusterClient = new ClientBuilder()
                   .UseLocalhostClustering()
                   .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ClusterClient.Connect();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await ClusterClient.Close();
        }
    }
}
