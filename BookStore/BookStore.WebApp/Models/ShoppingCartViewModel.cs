﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; }
    }
}