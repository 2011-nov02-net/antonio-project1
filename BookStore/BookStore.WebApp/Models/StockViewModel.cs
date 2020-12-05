using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class StockViewModel
    {

        [Display(Name = "Book Title")]
        public string Book { get; set; }

        [Display(Name = "In Stock")]
        [Required, RegularExpression("[A-Z].*")]
        public int Quantity { get; set; }
    }
}
