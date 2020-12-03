using System.Collections.Generic;

namespace BookStore.WebApp.Models
{
    public class PurchaseViewModel
    {
        public LocationViewModel Location { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public IEnumerable<OrderLineViewModel> OrderLines { get; set; }
    }
}
