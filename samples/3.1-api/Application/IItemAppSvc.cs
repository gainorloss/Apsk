using _3._1_api.Data;
using Apsk.AspNetCore;
using System.Collections.Generic;

namespace _3._1_api.Application
{
    public interface IItemAppSvc
    {
        RestResult ListCatalogItems();
    }
}
