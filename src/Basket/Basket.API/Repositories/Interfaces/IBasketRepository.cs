
using Basket.API.Data;
using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string username);
        Task<BasketCart> UpdateBasket(BasketCart basket);

        Task<bool> DeleteBasket(string username);
        
    }
    
}