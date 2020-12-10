using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class StockViewModel
    {

        [Display(Name = "Book Title")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Book { get; set; }

        [Display(Name = "In Stock")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }

        public int CurrentLocationID {get;set;}
        public string ISBN { get; set; }
    }
}
