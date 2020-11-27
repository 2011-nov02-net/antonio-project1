using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Models
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public string MyStoreLocation { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public IEnumerable<LocationViewModel> allLocations { get; set; }
    }
}
