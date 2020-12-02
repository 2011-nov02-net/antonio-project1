using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class LocationViewModel
    {
        [Display(Name = "Location ID")]
        public int Id { get; set; }

        [Display(Name = "Location Name")]
        [Required, RegularExpression("[A-Z].*")]
        public string Name { get; set; }

        public IEnumerable<StockViewModel> Inventory { get; set; }

    }
}
