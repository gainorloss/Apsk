using Infrastructure.Annotations;
using Quartz;
using System.Threading.Tasks;

namespace BootSharp.API.Jobs
{
    //[Component]
    public class HelloJob : IJob
    {
        [Scheduled(Cron = "0/5 * * * * ?")]
        public Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine($"德玛西亚万岁");
            return Task.CompletedTask;
        }
    }
}
