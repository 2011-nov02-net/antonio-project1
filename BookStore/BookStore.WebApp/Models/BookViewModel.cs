using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class BookViewModel
    {
        [Display(Name ="ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Required, RegularExpression("[A-Z].*")]
        public string AuthorLastName { get; set; }

        [Required, RegularExpression("[A-Z].*")]
        public string AuthorFirstName { get; set; }

        [Display(Name = "Author")]
        public string AuthorFullName { get => $"{AuthorFirstName} {AuthorLastName}"; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }
    }
}
