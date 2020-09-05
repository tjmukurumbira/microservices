using System;
using System.Threading.Tasks;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web
{
    public class ProductDetailModel : PageModel
    {
        private readonly IBasketApi basketApi;
        private readonly ICatalogApi catalogApi;

        public ProductDetailModel(IBasketApi basketApi, ICatalogApi catalogApi)
        {
            this.basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            this.catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
        }
         
        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await catalogApi.GetCatalog(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await catalogApi.GetCatalog(productId);

            var username = "svm";
            var basket = await basketApi.GetBasket(username);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black"

            });

            var result = await basketApi.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}