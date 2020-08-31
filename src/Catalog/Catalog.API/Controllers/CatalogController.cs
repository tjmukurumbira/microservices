using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(
            IProductRepository repository,
            ILogger<CatalogController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
             var products = await repository.GetProducts();

             return Ok(products);
        }
        

        [HttpGet("{id:length(24)}", Name= "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult<Product>> GetProductsById(string id)
        {
             var product = await repository.GetProduct(id);
             if (product == null)
              {
                  logger.LogError($"Product with id {id} not found.");
                   return NotFound();
              } 

             return Ok(product);
        }

        [Route("GetProductsByCategory/{categoryName}")]
        [HttpGet]       
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
             var products = await repository.GetProductsByCategory(categoryName);

             return Ok(products);
        }


        
        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
             await repository.Create(product);
             return CreatedAtRoute("GetProduct",new { id = product.Id}, product);            

        }

        [HttpPut]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
             return Ok( await repository.Update(product));
                    

        }

         [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async  Task<ActionResult> DeleteProduct(string id)
        {
             return Ok( await repository.Delete(id));
                    

        }



    }
}
