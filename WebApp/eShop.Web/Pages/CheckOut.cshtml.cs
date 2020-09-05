using System;
using System.Threading.Tasks;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketApi basketApi;
        private readonly ICatalogApi catalogApi;

        public CheckOutModel(IBasketApi basketApi, ICatalogApi catalogApi)
        {
            this.basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            this.catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var username = "james";
            Cart = await basketApi.GetBasket(username);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var username = "james";
            Cart = await basketApi.GetBasket(username);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.Username = username;
            Order.TotalPrice = Cart.TotalPrice;

            await basketApi.CheckoutBasket(Order);
                      
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}