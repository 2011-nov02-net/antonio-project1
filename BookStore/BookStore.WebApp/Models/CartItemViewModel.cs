using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class CartItemViewModel
    { 
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "ISBN is Required")]
        [MaxLength(15)]
        public string ISBN { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
    }
}
