using System;
using System.Collections.Generic;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            if (! products.Find(p=>true).Any())
            {
               products.InsertManyAsync(GetProducts());
            }
        }

        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>{
                new Product{
                    Name= "IPhone X",
                    Summary="",
                    Description="",
                    Price = 950.00M,
                    Category ="Smart Phones"

                },
                 new Product{
                    Name= "Samsung Note",
                    Summary="",
                    Description="",
                    Price = 950.00M,
                    Category ="Smart Phones"

                }
            };
        }
    }
}