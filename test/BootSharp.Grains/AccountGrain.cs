using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Annotations;
using Orleans;

namespace BootSharp.Grains
{
    [Component(ServiceType = typeof(IAccountGrain))]
    public class AccountGrain
         : Grain, IAccountGrain
    {
        private List<string> loginUsers = new List<string>();

        public Task<IEnumerable<string>> GetAllUsersAsync()
        {
            return Task.FromResult<IEnumerable<string>>(loginUsers);
        }

        public Task LoginAsync(string uid)
        {
            if (!loginUsers.Contains(uid))
                loginUsers.Add(uid);
            return Task.CompletedTask;
        }
    }
}
