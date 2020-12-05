using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Interfaces
{
    public interface ICartRepository
    {
        bool AddCartItem(Customer customer, Book book, int quantity);
        bool RemoveCartItem(Customer customer, Book book, int quantity);
    }
}
