using Basket.API.Data;
using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext context;

        public BasketRepository(IBasketContext context)
        {
            this.context = context;
        }
        public async Task<BasketCart> GetBasket(string username)
        {
            var basket =  await context.Redis.StringGetAsync(username);
            if (basket.IsNullOrEmpty) 
            {
                return null;
            }
            return JsonConvert.DeserializeObject<BasketCart>( basket);
        }
       public async Task<BasketCart> UpdateBasket(BasketCart basket)
       {
            var updated = await context.Redis.StringSetAsync(basket.Username,JsonConvert.SerializeObject (basket));

            if (!updated) 
            {
                return null;
            }
            return await GetBasket(basket.Username);
       }

        public async Task<bool> DeleteBasket(string username)
        {
            return await context.Redis.KeyDeleteAsync(username);
        }
        
    }
    
}