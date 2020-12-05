using BookStore.Data.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly StoreContext _context;

        /// <summary>
        /// A repository managing data access for Store objects,
        /// using Entity Framework.
        /// </summary>
        public CartRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCartItem(Customer customer, Book book, int quantity)
        {
            var db_cartItem = new CartitemEntity
            {
                BookIsbn = book.ISBN,
                Quantity = quantity,
                ShoppingcartId = customer.MyCart.ID
            };

            // Add the new entity to the context to send over to the database
            _context.Add(db_cartItem);

            // I am using the aproach of sending the data over after each change instead of having a universal save button
            _context.SaveChanges();
        }

        public ShoppingCart GetShoppingCartByCustomerID(int customerID)
        {
            var db_cart = _context.Shoppingcarts.Include(i => i.Cartitems).FirstOrDefault(sc => sc.CustomerId == customerID);

            return new ShoppingCart
            {
                ID = db_cart.CartId,
                DateCreated = (DateTime)db_cart.CreateData,
                CartItems = db_cart.Cartitems.Select(ci => new CartItem
                {
                    ID = ci.ItemId,
                    Book = Book.GetBookFromLibrary(ci.BookIsbn),
                    Quantity = ci.Quantity,
                })
            };
        }

        public void RemoveCartItem(CartItem item)
        {
            var db_cartItem_rm = new CartitemEntity
            {
                ItemId = item.ID
            };

            _context.Attach(db_cartItem_rm);
            _context.Remove(db_cartItem_rm);

            _context.SaveChanges();
        }
    }
}
