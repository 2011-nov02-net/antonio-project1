﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public DateTime DateCreated { get; set; }
        public int NumberOfItemsInCart { get => CartItems.ToList().Count; }

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
