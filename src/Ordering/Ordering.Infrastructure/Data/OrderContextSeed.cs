using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry= 0) 
        {
            int retryCount = 0;
            try
            {
                orderContext.Database.Migrate();
                if (!orderContext.Orders.Any()) 
                {
                    orderContext.Orders.AddRange(CreateOrders());
                }
                orderContext.SaveChanges();

            }
            catch (Exception ex)
            {
                if (retryCount < retry.Value) 
                {
                    retryCount++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(orderContext, loggerFactory, retry);
                }
               
            }
        }

        private static List<Order> CreateOrders()
        {
            return new List<Order> 
            {
                new Order
                {
                    Username="james",
                    Email ="james@gmailc.com",
                    Firstname="james",
                    Lastname ="mukuru",
                    TotalPrice =500,
                    
                }
            };
        }
    }
}
