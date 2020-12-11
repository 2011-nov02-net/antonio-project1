using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class HomeViewModel
    {
        public Dictionary<string, decimal> LocationSales { get; set; }

        [Display(Name="Location with the most sales: ")]
        public string LocationWithMostSales { get; set; }
    }
}
