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
    public class CatalogApi : BaseHttpClientFactory, ICatalogApi
    {
        private readonly IApiSettings settings;      

        public CatalogApi(IApiSettings settings, IHttpClientFactory clientFactory): base(clientFactory)
        {
            this.settings = settings;
            
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                            .SetPath(settings.CatalogPath)
                                            .HttpMethod(HttpMethod.Post)                                           
                                            .GetHttpMessage();
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<CatalogModel>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                            .SetPath(settings.CatalogPath)
                                            .HttpMethod(HttpMethod.Get)
                                            .GetHttpMessage();
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                           .SetPath(settings.CatalogPath)
                                           .AddToPath(id)
                                           .HttpMethod(HttpMethod.Get)
                                           .GetHttpMessage();
            return await SendRequest<CatalogModel>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                                           .SetPath(settings.CatalogPath)
                                           .AddToPath("GetProductsByCategory")
                                           .AddToPath(category)
                                           .HttpMethod(HttpMethod.Get)
                                           .GetHttpMessage();
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public override HttpRequestBuilder GetHttpRequestBuilder(string path)
        {
            throw new NotImplementedException();
        }
    }
}
