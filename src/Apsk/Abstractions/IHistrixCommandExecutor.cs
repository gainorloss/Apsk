using System;
using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    public interface IHistrixCommandExecutor
    {
        void Execute(Action action);
        void Execute(Func<Task> action);
    }
}
