using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
