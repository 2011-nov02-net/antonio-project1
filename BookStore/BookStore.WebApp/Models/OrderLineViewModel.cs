using System.ComponentModel.DataAnnotations;

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
