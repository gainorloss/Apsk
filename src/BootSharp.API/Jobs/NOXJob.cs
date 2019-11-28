using System.Threading.Tasks;
using Infrastructure.Annotations;
using Quartz;

namespace BootSharp.API.Jobs
{
    [Component]
    public class NOXJob
        : IJob
    {
        [Scheduled(Cron ="0/5 * * * * ?")]
        public Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine($"诺克萨斯，永不后退");
            return Task.CompletedTask;
        }
    }
}
