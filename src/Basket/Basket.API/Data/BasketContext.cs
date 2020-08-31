using Basket.API.Data.Interfaces;
using StackExchange.Redis;
namespace Basket.API.Data
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer connection;

        public BasketContext(ConnectionMultiplexer connection)
        {
            this.connection = connection;
            Redis = connection.GetDatabase();
        }
        public IDatabase Redis { get; }
    }
    
}