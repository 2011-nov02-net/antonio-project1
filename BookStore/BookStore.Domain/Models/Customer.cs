using System;
using System.Collections.Generic;

namespace BookStore.Domain.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public int ID { get; set; }
        public virtual Location MyStoreLocation { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public void AddOrderToHistory(Order order)
        {
            Orders.Add(order);
        }

        public override string ToString()
        {
            return $"ID: {ID}\tName: {Name}";
        }
    }
}
