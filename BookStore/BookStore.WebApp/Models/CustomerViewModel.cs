using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Names can't be longer than 50 characters.")]
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Names can't be longer than 50 characters.")]
        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string Name { get => FirstName + " " + LastName; }

        [Display(Name = "My Store Location")]
        [Required(ErrorMessage = "You must pick a location for your store.")]
        public string MyStoreLocation { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public IEnumerable<LocationViewModel> allLocations { get; set; }

        public string SearchString { get; set; }
    }
}
