using eShop.Web.APICollection.Infrastructure;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using eShop.Web.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Web.APICollection
{
    public class BasketApi : BaseHttpClientFactory, IBasketApi
    {
        private readonly IApiSettings settings;

        public BasketApi(IApiSettings settings, IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.settings = settings;

        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                          .SetPath(settings.CatalogPath)
                                          .AddToPath("Checkout")
                                          .HttpMethod(HttpMethod.Post)
                                          .GetHttpMessage();
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
             await SendRequest<BasketCheckoutModel>(message);
        }

        public async Task<BasketModel> GetBasket(string username)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                           .SetPath(settings.BasketPath)
                                           .AddToPath(username)
                                           .HttpMethod(HttpMethod.Get)
                                           .GetHttpMessage();
            return await SendRequest<BasketModel>(message);
        }

        public override HttpRequestBuilder GetHttpRequestBuilder(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                           .SetPath(settings.CatalogPath)
                                           .HttpMethod(HttpMethod.Post)
                                           .GetHttpMessage();
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<BasketModel>(message);
        }
    }
}
