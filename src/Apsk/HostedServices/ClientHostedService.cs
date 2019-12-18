using Microsoft.Extensions.Hosting;
using Orleans;
using System.Threading;
using System.Threading.Tasks;

namespace Apsk
{
    public class ClientHostedService
        : IHostedService
    {
        public readonly IClusterClient ClusterClient;
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
