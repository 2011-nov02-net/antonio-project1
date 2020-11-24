using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class StockViewModel
    {

        [Display(Name = "Book Title")]
        public string Book { get; set; }

        [Display(Name = "Quantity")]
        [Required, RegularExpression("[A-Z].*")]
        public int Quantity { get; set; }
    }
}
