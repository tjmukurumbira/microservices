using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Models
{
    

    public class BasketItemModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class BasketModel
    {
        public string Username { get; set; }

        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();

        public BasketModel(string username)
        {
            this.Username = username;
        }

        public BasketModel()
        {

        }

        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(i => i.Price * i.Quantity);
            }
        }

    }

}
