using Microsoft.Extensions.Hosting;
using Orleans.Hosting;
using System;

namespace BootSharp.Silo
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                 .UseOrleans(builder =>
                 {
                     builder.UseLocalhostClustering()
                     .AddMemoryGrainStorageAsDefault();
                 })
                 .RunConsoleAsync();
        }
    }
}
