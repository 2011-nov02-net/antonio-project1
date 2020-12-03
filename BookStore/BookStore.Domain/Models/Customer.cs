using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public int ID { get; set; }
        public virtual Location MyStoreLocation { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public void AddOrderToHistory(Order order)
        {
            Orders.ToList().Add(order);
        }

        public override string ToString()
        {
            return $"ID: {ID}\tName: {Name}";
        }
    }
}
