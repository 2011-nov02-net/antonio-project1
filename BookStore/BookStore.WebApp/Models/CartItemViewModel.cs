using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class CartItemViewModel
    {
        public string ISBN { get; set; }

        public int Quantity { get; set; }
    }
}
