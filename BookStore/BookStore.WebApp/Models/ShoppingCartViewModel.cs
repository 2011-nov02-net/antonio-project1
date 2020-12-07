﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class ShoppingCartViewModel
    {
        public int ID { get; set; }
        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        [DataType(DataType.Currency)]
        public decimal CartTotal {get;set;}
    }
}
