using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web
{
    public class OrderModel : PageModel
    {
        private readonly IOrderApi orderApi;

        public OrderModel(IOrderApi orderApi)
        {
          this.orderApi = orderApi ?? throw new ArgumentNullException(nameof(orderApi));
        }

        public IEnumerable<OrdersModel> Orders { get; set; } = new List<OrdersModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await orderApi.GetOrdersByUsername("james");

            return Page();
        }       
    }
}