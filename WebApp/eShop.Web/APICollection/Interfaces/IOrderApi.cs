using eShop.Web;
using eShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.APICollection.Interfaces
{
     public interface IOrderApi
    {
        Task<IEnumerable<OrdersModel>> GetOrdersByUsername(string username);
    }
}
