using BookStore.Domain.Models;
using System.Collections.Generic;

namespace BookStore.Domain.Interfaces
{
    public interface IStoreRepository
    {
        IEnumerable<Models.Location> GetAllLocations(string search = null);
        void PlaceAnOrderForACustomer(Order m_order);
        public Models.Customer GetCustomerWithLocationAndInventory(int id);
        void AddACustomer(Customer customer);
        public IEnumerable<Models.Customer> FindCustomerByName(string[] search);
        Domain.Models.Order GetDetailsForOrder(int ordernumber);
        IEnumerable<Order> GetOrderHistoryByLocationID(int locationID);
        Domain.Models.Customer GetOrderHistoryByCustomer(int id);
        void Save();
        IEnumerable<Models.Stock> GetStocksForLocation(int locationID);
        IEnumerable<Domain.Models.Book> FillBookLibrary();
        IEnumerable<Domain.Models.Customer> GetCustomers();
        Book GetBook(string isbn);
        Dictionary<string, int> GetLocationsIfStocksExistForISBN(int locationID, string search);

    }
}
