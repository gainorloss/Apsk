using _3._1_api.Data;
using Apsk.Annotations;
using Apsk.AspNetCore;
using Apsk.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _3._1_api.Application
{
    [RestController("item")]
    [Service]
    public class ItemAppSvc
        : RestController, IItemAppSvc
    {
        private readonly static List<Item> Items = new List<Item>();

        static ItemAppSvc()
        {
            Items.Add(new Item() { Id = 1, BrowseTimes = 10, Name = "iphone 7 plus", Price = 30, Title = "iphone 7 plus" });
            Items.Add(new Item() { Id = 2, BrowseTimes = 20, Name = "iphonex", Price = 40, Title = "iphonex" });
            Items.Add(new Item() { Id = 3, BrowseTimes = 30, Name = "iphone11", Price = 50, Title = "iphone11" });
        }

        [HttpGet]
        public RestResult ListCatalogItems()
        {
            return Success(Items);
        }
    }
}
