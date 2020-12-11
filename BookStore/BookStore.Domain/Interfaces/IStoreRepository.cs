using BookStore.Domain.Models;
using System.Collections.Generic;

namespace BookStore.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Dictionary<string, decimal> GetLocationNamesWithTotalSales();
        IEnumerable<Domain.Models.Book> GetBestSellersList();
        IEnumerable<Location> GetAllLocations(string search = null);
        int PlaceAnOrderForACustomer(Order m_order);
        public Customer GetCustomerWithLocationAndInventory(int id);
        void AddACustomer(Customer customer);
        public IEnumerable<Customer> FindCustomerByName(string[] search);
        Order GetDetailsForOrder(int ordernumber);
        IEnumerable<Order> GetOrderHistoryByLocationID(int locationID);
        Customer GetOrderHistoryByCustomer(int id);
        void Save();
        IEnumerable<Stock> GetStocksForLocation(int locationID);
        IEnumerable<Book> FillBookLibrary(string isbn = null);
        IEnumerable<Customer> GetCustomers();
        Dictionary<string, int> GetLocationsIfStocksExistForISBN(int locationID, string search);

    }
}
