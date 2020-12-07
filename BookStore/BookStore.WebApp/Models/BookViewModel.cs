using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class BookViewModel
    {
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "ISBN is Required")]
        [MaxLength(15, ErrorMessage = "ISBN can't be longer than 15 characters")]
        public string ISBN { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author's last name is Required"), RegularExpression("[A-Z].*", ErrorMessage = "Name must contain letters.")]
        [MaxLength(50)]
        public string AuthorLastName { get; set; }

        [Required(ErrorMessage = "Author's first name is Required"), RegularExpression("[A-Z].*", ErrorMessage = "Name must contain letters.")]
        [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string AuthorFirstName { get; set; }

        [Display(Name = "Author")]
        public string AuthorFullName { get => $"{AuthorFirstName} {AuthorLastName}"; }

        [Display(Name = "Price")]
        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Dictionary<string, int> LocationsWithStock { get; set; }
    }
}
