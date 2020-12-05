using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Interfaces
{
    public interface ICartRepository
    {
        void AddCartItem(Customer customer, Book book, int quantity);
        void RemoveCartItem(CartItem item);
        ShoppingCart GetShoppingCartByCustomerID(int customerID);
    }
}
