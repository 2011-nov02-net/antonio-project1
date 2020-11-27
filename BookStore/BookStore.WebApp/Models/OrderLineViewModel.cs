using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class OrderLineViewModel
    {
        [Display(Name = "ISBN")]
        public string BookISBN { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Line Cost")]
        public decimal LineCost { get; set; }
    }
}
