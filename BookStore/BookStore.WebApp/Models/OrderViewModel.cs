﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        [Display(Name = "Date Placed")]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
    }
}
