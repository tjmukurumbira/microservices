using eShop.Web;
using eShop.Web.APICollection.Infrastructure;
using eShop.Web.APICollection.Interfaces;
using eShop.Web.Models;
using eShop.Web.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Web.APICollection
{
    public class OrderApi : BaseHttpClientFactory, IOrderApi
    {
        private readonly IApiSettings settings;

        public OrderApi(IApiSettings settings, IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.settings = settings;

        }

        public override HttpRequestBuilder GetHttpRequestBuilder(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrdersModel>> GetOrdersByUsername(string username)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                           .SetPath(settings.OrderPath)
                                           .HttpMethod(HttpMethod.Get)
                                           .GetHttpMessage();
            return await SendRequest<IEnumerable<OrdersModel>>(message);
        }
    }
}
