using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShop.Web.APICollection.Infrastructure
{
    public class HttpRequestBuilder
    {
        private readonly HttpRequestMessage request;
        private string baseAddress;
        private readonly ApiBuilder apiBuilder;
        public HttpRequestBuilder(string uri) : this(new ApiBuilder(uri)) { }
        public HttpRequestBuilder(ApiBuilder apiBuilder)
        {
            this.request = new HttpRequestMessage();
            this.apiBuilder = apiBuilder;
            this.baseAddress = apiBuilder.GetLeftPart();
        }
        public HttpRequestBuilder HttpMethod(HttpMethod httpMethod)
        {
           this.request.Method = httpMethod;
            return this;
        }
        public HttpRequestBuilder Headers(Action<HttpRequestHeaders> funcOfHeaders)
        {
            funcOfHeaders(this.request.Headers);
            return this;
        }
        public HttpRequestBuilder Headers(NameValueCollection headers)
        {
            this.request.Headers.Clear();
            foreach (var item in headers.AllKeys)
            {
                this.request.Headers.Add(item, headers[item]);
            }
            return this;
        }
        public HttpRequestBuilder AddToPath(string path)
        {
            this.apiBuilder.AddToPath(path);
            this.request.RequestUri = this.apiBuilder.GetUri();
            return this;
        }
        public HttpRequestBuilder SetPath(string path)
        {
            this.apiBuilder.SetPath(path);
            this.request.RequestUri = this.apiBuilder.GetUri();
            return this;
        }
        public HttpRequestBuilder Content(HttpContent content)
        {
            this.request.Content = content;
            return this;
        }
        public HttpRequestBuilder RequestUri(Uri uri)
        {
            this.request.RequestUri = new ApiBuilder(uri.ToString()).GetUri();
            return this;
        }
        public HttpRequestBuilder RequestUri(string uri)
        {
            return RequestUri(new Uri(uri));
        }
        public HttpRequestBuilder BaseAddress(string address)
        {
            this.baseAddress = address;
            return this;
        }
        public HttpRequestBuilder Subdomain(string subdomain)
        {
            this.apiBuilder.SetSubdomain(subdomain);
            this.request.RequestUri = this.apiBuilder.GetUri();
            return this;
        }
        public HttpRequestBuilder AddQueryString(string name, string value)
        {
            this.apiBuilder.AddQueryString(name, value);
            this.request.RequestUri = this.apiBuilder.GetUri();
            return this;
        }
        public HttpRequestBuilder SetQueryString(string qs)
        {
            this.apiBuilder.QueryString(qs);
            this.request.RequestUri = this.apiBuilder.GetUri();
            return this;
        }
        public HttpRequestMessage GetHttpMessage()
        {
            return this.request;
        }
        public ApiBuilder GetApiBuilder()
        {
            return new ApiBuilder(this.request.RequestUri.ToString());
        }
    }
}
