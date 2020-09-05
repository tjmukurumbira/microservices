using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web
{
    public class ProductModel : PageModel
    {
        private readonly IBasketApi basketApi;
        private readonly ICatalogApi catalogApi;

        public ProductModel(IBasketApi basketApi, ICatalogApi catalogApi)
        {
            this.basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            this.catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
        }
        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string category)
        {
            CategoryList = new List<string>() { };

            if (! string.IsNullOrWhiteSpace(category))
            {
                ProductList = await catalogApi.GetCatalogByCategory(category);
               // SelectedCategory = CategoryList.FirstOrDefault(c => c.Id == categoryId.Value)?.Name;
            }
            else
            {
                ProductList = await catalogApi.GetCatalog();
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