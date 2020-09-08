using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBasketApi basketApi;
        private readonly ICatalogApi catalogApi;

        public IndexModel(IBasketApi basketApi, ICatalogApi catalogApi)
        {
            this.basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            this.catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await catalogApi.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await catalogApi.GetCatalog(productId);

            var username = "svm";
            var basket = await basketApi.GetBasket(username);

            basket.Items.Add(new BasketItemModel{
            ProductId= productId,
            ProductName  = product.Name,
            Price = product.Price,
            Quantity = 1,
            Color =  "Black"
            
            });

            var result = await basketApi.UpdateBasket(basket);
            return RedirectToPage("Cart");

           
        }
    }
}
