using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace eShop.Web.APICollection.Infrastructure
{
    public class ApiBuilder
    {
        private readonly string url;
        private readonly UriBuilder builder;
       

        public ApiBuilder(string url)
        {
            this.url = url ?? throw new ArgumentNullException(nameof(url));
            this.builder = new UriBuilder(url);
        }
        public Uri GetUri() => builder.Uri;

        public ApiBuilder Scheme(string scheme) 
        {
            this.builder.Scheme = scheme;
            return this;
        }

        public ApiBuilder Host(string host)
        {
            this.builder.Host = host;
            return this;
        }
        public ApiBuilder Port(int port)
        {
            this.builder.Port = port;
            return this;
        }
        public ApiBuilder AddToPath(string path)
        {
            IncludePath(path);
            return this;
        }
        public ApiBuilder SetPath(string path)
        {
            builder.Path = path;
            return this;
        }

        private void IncludePath(string path)
        {
            if (string.IsNullOrEmpty(builder.Path) || builder.Path == "/")
            {
                builder.Path = path;
            }
            else {
                var newPath = $"{builder.Path}/{path}";
                builder.Path = newPath.Replace("//", "/");
            
            }
        }

        public ApiBuilder Fragment(string fragment) 
        {
            builder.Fragment = fragment;
            return this;
        }

        public ApiBuilder SetSubdomain(string subdomain) 
        {
            builder.Host = string.Concat(subdomain, ".", new Uri(url).Host);
            return this;
        }

        public bool HasSubdomain() 
        {
            return builder.Uri.HostNameType == UriHostNameType.Dns &&
               builder.Uri.Host.Split('.').Length > 2;

        }

        public ApiBuilder AddQueryString(string name, string value) 
        {
            var qsNv = HttpUtility.ParseQueryString(builder.Query);
            qsNv[name] = string.IsNullOrEmpty(qsNv[name]) ? value : string.Concat(qsNv[name],",",value);

            builder.Query = qsNv.ToString();
            return this;
        }

        public ApiBuilder QueryString(string queryString) 
        {
            if (!string.IsNullOrEmpty(queryString)) 
            {
                builder.Query = queryString;
            }
            return this;
        }

        public ApiBuilder Username(string username) 
        {
            builder.UserName = username;
            return this;
        }

        public ApiBuilder Password(string password) 
        {
            builder.Password = password;
            return this;
        }

        public string GetLeftPart()
        {
            return builder.Uri.GetLeftPart(UriPartial.Path);
        }
    }
}
