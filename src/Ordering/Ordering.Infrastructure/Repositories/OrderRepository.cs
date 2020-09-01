using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Entities.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username)
        {
           return await orderContext.Orders
                                        .Where(o => o.Username == username)
                                        .ToListAsync();
        }
    }
}
