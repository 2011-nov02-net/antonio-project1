﻿using BookStore.Data.Entities;
using BookStore.Data.Mappers;
using BookStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext _context;

        /// <summary>
        /// A repository managing data access for Store objects,
        /// using Entity Framework.
        /// </summary>
        public StoreRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Dictionary<string, decimal> GetLocationNamesWithTotalSales()
        {
            Dictionary<string, decimal> locationSales =  _context.Locations.ToDictionary(l => l.Name, y=>  Convert.ToDecimal(0));

            foreach (var key in locationSales.Keys.ToList() ) {
                locationSales[key] = _context.Orders.Include(l=>l.Location).Include(ol=>ol.Orderlines).Select(MapperOrder.MapOrderWithOrderLinesAndLocation).Where(l=>l.LocationPlaced.LocationName==key).Sum(o=>o.TotalCost);
            }
            return locationSales;
        }

        public IEnumerable<Domain.Models.Book> GetBestSellersList() 
        {
            var orderLines = _context.Orderlines.Select(MapperOrderLine.Map);
            var isbns = _context.Books.Include(g=>g.Genre).Select(MapperBook.Map).ToDictionary(b => b.ISBN, y => 0);
            foreach (var ol in orderLines) {
                foreach (var key in isbns.Keys.ToList())
                    if (ol.BookISBN == key) {
                        isbns[key] += ol.Quantity;
                    }
            }
            var topFiveSellers = new List<Domain.Models.Book>();
            foreach (var total in isbns.OrderByDescending(key => key.Value).Take(5)) {
                topFiveSellers.Add(_context.Books.Select(MapperBook.Map).First(b=>b.ISBN == total.Key));
            }
            return topFiveSellers;
        }

        /// <summary>
        /// This function can take a string or not. If it does then it will return a location by itself.
        /// If it does not contain a string then it returns all the locations in the database mapped to
        /// Domain.Models.Location objects to print.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The list of locations</returns>
        public IEnumerable<Domain.Models.Location> GetAllLocations(string search = null)
        {
            IQueryable<Entities.LocationEntity> dbLocations = _context.Locations;

            // This is were we check if it is one location or all
            if (search != null)
            {
                dbLocations = dbLocations.Where(i => i.Name.Contains(search));
            }

            return dbLocations.Select(MapperLocation.Map);
        }

        public Dictionary<string, int> GetLocationsIfStocksExistForISBN(int locationID, string search)
        {
            IEnumerable<Entities.LocationEntity> dbLocations = _context.Locations
                .Include(s => s.Inventories);

            Dictionary<string, int> nameWithQuantity = new Dictionary<string, int>();
            foreach (var l in dbLocations)
            {
                string name = l.Name;
                if (l.Id == locationID)
                {
                    foreach (var s in l.Inventories)
                    {
                        if (s.Quantity > 0 && s.BookIsbn.Equals(search))
                        {
                            nameWithQuantity[name] = (int)s.Quantity;
                        }
                    }
                }
            }

            return nameWithQuantity;
        }

        /// <summary>
        /// The purpose of this class is to insert new a new order into the database. 
        /// </summary>
        /// <param name="Order">Type Domain.Models.Order. It will contain all data about customer, location, and a list of orderlines.</param>
        public int PlaceAnOrderForACustomer(Domain.Models.Order m_order)
        {
            // Create the Entity item to be put into the database
            Entities.OrderEntity order;
            order = MapperOrder.MapOrderWithOrderLines(m_order);

            // We need to grab the entity objects from the database for the inventory rows for the given location.
            // This is so we can update them accordingly.
            IEnumerable<Entities.InventoryEntity> dbStocks = _context.Inventories.Where(i => i.LocationId == m_order.LocationPlaced.ID);

            // Since we are returned all the rows of inventory we need to iterate through each one to update it
            // This is done no matter if there was 1 purchase or many changing the inventory just to be sure 
            // everything is updated correctly.
            foreach (Entities.InventoryEntity i in dbStocks)
            {
                // We also need to iterate through all the Domain.Models.Stock list for the location
                foreach (var orderLine in m_order.Purchase)
                {
                    // An extra measure is taken here just to be sure that only books that exists in the database are being changed.
                    if (orderLine.BookISBN == i.BookIsbn)
                    {
                        i.Quantity -= orderLine.Quantity;
                    }
                }
            }

            // Add the new order and orderlines to the database
            _context.Add(order);

            // Save those changes
            Save();
            return order.Id;
        }

        /// <summary>
        /// Right now this is mainly a helper method when placing the order. This is because this returns a Domain.Models.Customer object
        /// That is manipulated by the c# code. The intention was to get the Customer and then set the location and it's inventory
        /// </summary>
        /// <param name="name">Two strings that are valid names.</param>
        /// <returns></returns>
        public Domain.Models.Customer GetCustomerWithLocationAndInventory(int id)
        {
            // first we create our db customer to check if we find it
            Entities.CustomerEntity dbCustomer;

            // if we do then we assign it to the customer
            dbCustomer = _context.Customers
                .Include(l => l.Location)
                .Include(sc => sc.Shoppingcarts)
                .ThenInclude(ci => ci.Cartitems).First(c => c.Id == id);

            // since we found it we map the new customer with the location
            Domain.Models.Customer m_customer = MapperCustomer.MapCustomerWithLocation(dbCustomer);

            // then we get the stocks for the location
            m_customer.MyStoreLocation.Inventory = GetStocksForLocation(m_customer.MyStoreLocation.ID).ToList();

            return m_customer;
        }

        /// <summary>
        /// This is another help method that just gets the stocks for a given location from the db.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of stocks to be assigned to the object that called it.</returns>
        public IEnumerable<Domain.Models.Stock> GetStocksForLocation(int locationID)
        {
            // since it is a location that exists we don't have to do much exception handling and we just get the inventories for the location including the book table
            IQueryable<Entities.InventoryEntity> stocks = _context.Inventories.Include(b => b.BookIsbnNavigation).ThenInclude(g => g.Genre).Where(i => i.LocationId == locationID);
            List<Domain.Models.Stock> m_stocks = new List<Domain.Models.Stock>();

            // assign each stock from the list of stocks to a model
            foreach (Entities.InventoryEntity s in stocks)
            {
                m_stocks.Add(MapperInventory.Map(s));
            }

            return stocks.Select(MapperInventory.Map);
        }

        /// <summary>
        /// Add a new Customer from Models to Database.
        /// </summary>
        /// <param name="customer"> This is the new Model to be put into the database. It only has a firstname and last name.</param>
        public void AddACustomer(Domain.Models.Customer customer)
        {
            // Create the Entity item to be put into the database
            Entities.CustomerEntity entity;

            // Since the database handles the ID setting with identity, we only need to assign the new entity the firstname and the lastname
            // Maybe in the future we could add a way to change the location, but for now the database sets the location to the default 1.
            entity = MapperCustomer.Map(customer);

            // Add the new entity to the context to send over to the database
            _context.Add(entity);
            Save();

            // Create their shopping cart
            Entities.ShoppingcartEntity shoppingcartEntity = new Entities.ShoppingcartEntity
            {
                CustomerId = entity.Id
            };
            _context.Add(shoppingcartEntity);
            // I am using the aproach of sending the data over after each change instead of having a universal save button
            Save();
        }

        /// <summary>
        /// The purpose of this method is to search the db to return a model of the customer object given two strings as names
        /// </summary>
        /// <param name="search">Two strings a first and a last name</param>
        /// <returns>A customer object</returns>
        public IEnumerable<Domain.Models.Customer> FindCustomerByName(string[] search)
        {
            // Search the db and if someone is found assign it if no one was found assign null
            List<Entities.CustomerEntity> dbCustomer = _context.Customers.Where(c => (c.FirstName == search[0] && c.LastName == search[1])).ToList();

            // if it is null exit the method and return null
            if (dbCustomer == null)
            {
                return new List<Domain.Models.Customer>();
            }

            List<Domain.Models.Customer> m_customers = new List<Domain.Models.Customer>();
            foreach (Entities.CustomerEntity customer in dbCustomer)
            {
                m_customers.Add(MapperCustomer.Map(customer));
            }

            return m_customers;
        }

        public IEnumerable<Domain.Models.Customer> GetCustomers()
        {
            FillBookLibrary();
            return _context.Customers.Include(l => l.Location)
                    .Include(sc => sc.Shoppingcarts)
                    .ThenInclude(ci => ci.Cartitems).Select(MapperCustomer.MapCustomerWithLocation);
        }
        /// <summary>
        /// The purpose of this method is to return the string version of an order, given an order number.
        /// It not only returns the information on the customer but also each order line.
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns>A string version of an order summary.</returns>
        public Domain.Models.Order GetDetailsForOrder(int ordernumber)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the Domain with the relevant books
            FillBookLibrary();

            // Create the order objects to be filled
            Entities.OrderEntity dbOrder;
            Domain.Models.Order m_order;

            // Try to see if the order even exists if it does then assign it

            dbOrder = _context.Orders
                .Include(ol => ol.Orderlines)
                .Include(c => c.Customer)
                .Include(l => l.Location)
                .First(o => o.Id == ordernumber);

            m_order = MapperOrder.MapOrderWithLocationCustomerAndOrderLines(dbOrder);


            return m_order;
        }

        /// <summary>
        /// The purpose is to turn the db information for a given locations order history into readable text.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public IEnumerable<Domain.Models.Order> GetOrderHistoryByLocationID(int locationID)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the Domain with the relevant books
            FillBookLibrary();

            // Find if the location exists and include all information including orders and their orderlines
            Entities.LocationEntity dbLocation = _context.Locations
                .Include(o => o.Orders)
                .ThenInclude(c => c.Orderlines)
                .FirstOrDefault(l => l.Id == locationID);

            // If it doesn't exist then return that the location does not exist.
            if (dbLocation == null)
            {
                return new List<Domain.Models.Order>();
            }
            return dbLocation.Orders.Select(MapperOrder.MapOrderWithOrderLines);
        }

        public Domain.Models.Customer GetOrderHistoryByCustomer(int id)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the Domain with the relevant books
            FillBookLibrary();

            // Attempt to find the customer
            Entities.CustomerEntity dbCustomer = _context.Customers
                .Include(l => l.Location)
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .FirstOrDefault(c => c.Id == id);

            // If the customer was not found then let the user know
            if (dbCustomer == null)
            {
                return null;
            }

            // if one was found then map it to a usable object
            Domain.Models.Customer m_customer = MapperCustomer.MapCustomerWithOrders(dbCustomer);

            return m_customer;
        }

        /// <summary>
        /// The purpose of this class is to fill the static Domain in the models with the book information
        /// </summary>
        public IEnumerable<Domain.Models.Book> FillBookLibrary(string isbn = null)
        {
            IQueryable<BookEntity> dbBooks = _context.Books.Include(g => g.Genre);
            Domain.Models.Book.Library = dbBooks.Select(MapperBook.Map);
            // This is were we check if it is one location or all
            if (isbn != null)
            {
                dbBooks = dbBooks.Where(i => i.Isbn.Contains(isbn));
            }

            return dbBooks.Select(MapperBook.Map);
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
