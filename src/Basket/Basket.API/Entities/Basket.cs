using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }

         public string Country { get; set; }
        public string State { get; set; }

         public string ZipCode { get; set; }
        public string Cardname { get; set; }

         public string Cardnumber { get; set; }
        public string Expiration { get; set; }

         public string CVV { get; set; }
        public int PaymentMethod { get; set; }


    }
    public class BasketCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class BasketCart
    {
        public string Username { get; set; }

        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public BasketCart(string username)
        {
            this.Username = username;
        }

        public BasketCart()
        {

        }

        public decimal TotalPrice { 
            get
            {
                return Items.Sum(i=>i.Price* i.Quantity);
            } 
        }

    }

}