using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public DateTime DateCreated { get; set; }
        private int _itemCount;
        public int NumberOfItemsInCart { get => _itemCount; set => _itemCount = CartItems.ToList().Count; }
        private decimal _cartTotal;
        public decimal CartTotal { get { return _cartTotal; } set { _cartTotal = CartItems.Sum(b => b.Book.Price); } }

        public bool AddToCartAttempt(Book book, int quantity, Location location)
        {
            if (location.CheckStockForOrderAttempt(book, quantity))
            {
                AddToCart(new CartItem { Book = book, Quantity = quantity });
                return true;
            }
            return false;
        }

        private void AddToCart(CartItem item)
        {
            CartItems.ToList().Add(item);
        }

        public bool RemoveFromCart(CartItem item)
        {
            return CartItems.ToList().Remove(item);
        }
    }
}
