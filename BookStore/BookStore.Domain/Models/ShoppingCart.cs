using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class ShoppingCart
    {
        IEnumerable<CartItem> CartItems { get; set; }
        public int NumberOfItemsInCart { get => CartItems.ToList().Count; }
        public Customer Customer { get; set; }

        public void Checkout(Customer customer)
        {
        
        }

        public bool AddToCartAttempt(Book book, int quantity, Location location){
            return location.CheckStockForOrderAttempt(book, quantity);
        }
    }
}
