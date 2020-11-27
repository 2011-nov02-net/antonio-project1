using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        IEnumerable<Order> GetOrderHistoryByCustomer(int id);
        void Save();
        public IEnumerable<Models.Stock> GetStocksForLocation(int locationID);
        IEnumerable<Domain.Models.Book> FillBookLibrary();
        public IEnumerable<Domain.Models.Customer> GetCustomers();

    }
}
