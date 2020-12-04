using System.Collections.Generic;

namespace BookStore.WebApp.Models
{
    public class PurchaseViewModel
    {
        string LocationName { get; set; }
        public IEnumerable<StockViewModel> Inventory { get; set; }
    }
}
