using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string Name { get => FirstName + " " + LastName; }

        [Display(Name = "My Store Location")]
        [Required]
        public string MyStoreLocation { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public IEnumerable<LocationViewModel> allLocations { get; set; }

        public string SearchString { get; set; }
    }
}
