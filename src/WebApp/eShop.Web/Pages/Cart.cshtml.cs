using System;
using System.Linq;
using System.Threading.Tasks;

using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web
{
    public class CartModel : PageModel
    {
       
        private readonly IBasketApi basketApi;
        private readonly ICatalogApi catalogApi;

        public CartModel(IBasketApi basketApi, ICatalogApi catalogApi)
        {
            this.basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            this.catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketApi.GetBasket("james");     

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var username = "james";
            var basket = await basketApi.GetBasket(username);

            var item = basket.Items.FirstOrDefault(b=>b.ProductId == productId);
                
            basket.Items.Remove(item);

            var update = await basketApi.UpdateBasket(basket);
            
            return RedirectToPage();
        }
    }
}