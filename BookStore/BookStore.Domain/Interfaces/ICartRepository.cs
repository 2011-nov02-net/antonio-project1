using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface ICartRepository
    {
        void AddCartItem(Customer customer, Book book, int quantity);
        void RemoveCartItem(Customer customer, Book book, int quantity);
        ShoppingCart GetShoppingCartByCustomerID(int customerID);
        void EmptyCart(Customer customer);
    }
}
