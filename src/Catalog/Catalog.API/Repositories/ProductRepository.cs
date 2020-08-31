using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext context;

        public ProductRepository(ICatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Create(Product product)
        {
            await this.context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
             FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p=>p.Id, id);
             DeleteResult result = await context.Products.DeleteOneAsync(filter);

             return result.IsAcknowledged && result.DeletedCount>0;
    
            
        }

        public  async Task<Product> GetProduct(string id)
        {
            return await this.context
                                .Products
                                .Find(p=>p.Id==id)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
           return  await this.context
                                .Products
                                .Find(p=>true)
                                .ToListAsync();
        
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p=>p.Category, categoryName);
            return  await this.context
                                .Products
                                .Find(filter)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p=>p.Name, name);
            return  await this.context
                                .Products
                                .Find(filter)
                                .ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResults  = await context
                                            .Products
                                            .ReplaceOneAsync(filter: g=>g.Id ==product.Id, replacement: product);

            return updateResults.IsAcknowledged&& updateResults.ModifiedCount>0;
        }
    }
}