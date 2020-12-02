using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class PurchaseViewModel
    {
        public LocationViewModel Location {get;set;}
        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public IEnumerable<OrderLineViewModel> OrderLines { get; set; }
    }
}
