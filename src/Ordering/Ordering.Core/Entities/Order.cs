using Ordering.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities
{
    public class Order : Entity
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
}
