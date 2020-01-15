using System.Threading.Tasks;

namespace CatalogItems.API.Services
{
    public interface IUserService
    {
        Task<string> GetNameAsync();
    }
}
