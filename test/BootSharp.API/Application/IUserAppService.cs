using BootSharp.API.Models;
using Infrastructure.Web;
using System.Threading.Tasks;

namespace BootSharp.API.Application
{
    public interface IUserAppService
    {
        Task<RestResult> GetUserNameAsync();
    }
}
