using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.Grains
{
    public interface IAccountGrain
        : IGrainWithStringKey
    {
        Task LoginAsync(string uid);

        Task<IEnumerable<string>> GetAllUsersAsync();
    }
}
