using Apsk.Annotations;
using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;
using CatalogItems.API.Services;
using Projects.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.API.Application
{
    [RestController("apsk")]
    [Service]
    public class ItemAppService
        : RestController, IItemAppService
    {
        private readonly static List<Item> Items = new List<Item>();

        static ItemAppService()
        {
            Items.Add(new Item() { Id = 1, BrowseTimes = 10, Name = "iphone 7 plus", Price = 30, Title = "iphone 7 plus" });
            Items.Add(new Item() { Id = 2, BrowseTimes = 20, Name = "iphonex", Price = 40, Title = "iphonex" });
            Items.Add(new Item() { Id = 3, BrowseTimes = 30, Name = "iphone11", Price = 50, Title = "iphone11" });
        }

        private readonly IUserService _userService;
        public ItemAppService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<RestResult> ListAsync()
        {
            var name = await _userService.GetNameAsync();
            return Success(Items);
        }
    }
}
