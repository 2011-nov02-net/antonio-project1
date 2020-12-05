using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Order Total")]
        public decimal TotalCost { get; set; }

        public List<OrderLineViewModel> Purchase { get; set; }
    }
}
