using System.Threading.Tasks;

namespace BootSharp.ConsoleApp.AggregatesModel
{
    public interface IPersonRepository
    {
        Task CreatePersonAsync(Person person);
    }
}
