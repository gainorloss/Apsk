using System.Threading.Tasks;

namespace BootSharp.API.Application
{
    public interface IUserAppService
    {
        Task<string> GetUserNameAsync();
    }
}
