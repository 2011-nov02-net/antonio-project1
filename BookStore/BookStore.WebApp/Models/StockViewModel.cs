using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class StockViewModel
    {

        [Display(Name = "Book Title")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Book { get; set; }

        [Display(Name = "In Stock")]
        [RegularExpression("^[0-9]", ErrorMessage = "Must be a number.")]
        public int Quantity { get; set; }
    }
}
