using Apsk.AspNetCore;
using System.Threading.Tasks;

namespace Projects.API.Application
{
    public interface IItemAppService
    {
        Task<RestResult> ListAsync();
    }
}
