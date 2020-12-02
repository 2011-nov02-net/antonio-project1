using Microsoft.EntityFrameworkCore;
using BookStore.Data.Entities;
using BookStore.Data.Mappers;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// This function can take a string or not. If it does then it will return a location by itself.
        /// If it does not contain a string then it returns all the locations in the database mapped to
        /// Domain.Models.Location objects to print.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The list of locations</returns>
        public IEnumerable<Domain.Models.Location> GetAllLocations(string search = null)
        {
            IQueryable<LocationEntity> dbLocations = _context.Locations;

            // This is were we check if it is one location or all
            if (search != null)
            {
                dbLocations = dbLocations.Where(i => i.Name.Contains(search));
            }

            return dbLocations.Select(Mapper_Location.Map);
        }

        /// <summary>
        /// The purpose of this class is to insert new a new order into the database. 
        /// </summary>
        /// <param name="Order">Type Domain.Models.Order. It will contain all data about customer, location, and a list of orderlines.</param>
        public void PlaceAnOrderForACustomer(Domain.Models.Order m_order)
        {
            // Create the Entity item to be put into the database
            OrderEntity order;
            order = Mapper_Order.MapOrderWithOrderLines(m_order);

            // We need to grab the entity objects from the database for the inventory rows for the given location.
            // This is so we can update them accordingly.
            IEnumerable<InventoryEntity> dbStocks = _context.Inventories.Where(i => i.LocationId == m_order.LocationPlaced.ID);

            // Since we are returned all the rows of inventory we need to iterate through each one to update it
            // This is done no matter if there was 1 purchase or many changing the inventory just to be sure 
            // everything is updated correctly.
            foreach (InventoryEntity i in dbStocks)
            {
                // We also need to iterate through all the Domain.Models.Stock list for the location
                foreach (Domain.Models.Stock stock in m_order.LocationPlaced.Inventory)
                {
                    // An extra measure is taken here just to be sure that only books that exists in the database are being changed.
                    if (stock.Book.ISBN == i.BookIsbn)
                    {
                        // Set the new quantity
                        i.Quantity = stock.Quantity;
                    }
                }
            }

            // Add the new order and orderlines to the database
            _context.Add(order);

            // Save those changes
            Save();
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
            CustomerEntity dbCustomer = new CustomerEntity();
            try
            {
                // if we do then we assign it to the customer
                dbCustomer = _context.Customers.Include(l => l.Location).First(c => c.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                // if we don't then we return null 
                return null;
            }
            // since we found it we map the new customer with the location
            Domain.Models.Customer m_customer = Mapper_Customer.MapCustomerWithLocation(dbCustomer);

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
            IQueryable<InventoryEntity> stocks = _context.Inventories.Include(b => b.BookIsbnNavigation).Where(i => i.LocationId == locationID);
            List<Domain.Models.Stock> m_stocks = new List<Domain.Models.Stock>();

            // assign each stock from the list of stocks to a model
            foreach (InventoryEntity s in stocks)
            {
                m_stocks.Add(Mapper_Inventory.Map(s));
            }

            return stocks.Select(Mapper_Inventory.Map);
        }

        /// <summary>
        /// Add a new Customer from Models to Database.
        /// </summary>
        /// <param name="customer"> This is the new Model to be put into the database. It only has a firstname and last name.</param>
        public void AddACustomer(Domain.Models.Customer customer)
        {
            // Create the Entity item to be put into the database
            CustomerEntity entity;

            // Since the database handles the ID setting with identity, we only need to assign the new entity the firstname and the lastname
            // Maybe in the future we could add a way to change the location, but for now the database sets the location to the default 1.
            entity = Mapper_Customer.Map(customer);

            // Add the new entity to the context to send over to the database
            _context.Add(entity);

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
            List<CustomerEntity> dbCustomer = _context.Customers.Where(c => (c.FirstName == search[0] && c.LastName == search[1])).ToList();

            // if it is null exit the method and return null
            if (dbCustomer == null)
            {
                return null;
            }

            List<Domain.Models.Customer> m_customers = new List<Domain.Models.Customer>();
            foreach (CustomerEntity customer in dbCustomer)
            {
                m_customers.Add(Mapper_Customer.Map(customer));
            }

            return m_customers;
        }

        public IEnumerable<Domain.Models.Customer> GetCustomers()
        {
            return _context.Customers.Include(l => l.Location).Select(Mapper_Customer.MapCustomerWithLocation);
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
            OrderEntity dbOrder;
            Domain.Models.Order m_order;

            // Try to see if the order even exists if it does then assign it
            try
            {
                dbOrder = _context.Orders
                    .Include(ol => ol.Orderlines)
                    .Include(c => c.Customer)
                    .Include(l => l.Location)
                    .First(o => o.Id == ordernumber);

                m_order = Mapper_Order.MapOrderWithLocationCustomerAndOrderLines(dbOrder);
            }
            catch (InvalidOperationException ex)
            {
                // if it doesn't then return the error along with a descriptive text
                return null;
            }

            return Mapper_Order.MapOrderWithLocationCustomerAndOrderLines(dbOrder);
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
            LocationEntity dbLocation = _context.Locations
                .Include(o => o.Orders)
                .ThenInclude(c => c.Orderlines)
                .FirstOrDefault(l => l.Id == locationID);

            // If it doesn't exist then return that the location does not exist.
            if (dbLocation == null) { 
                return null;
            }
            return dbLocation.Orders.Select(Mapper_Order.MapOrderWithOrderLines);
        }

        public Domain.Models.Customer GetOrderHistoryByCustomer(int id)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the Domain with the relevant books
            FillBookLibrary();

            // Attempt to find the customer
            CustomerEntity dbCustomer = _context.Customers
                .Include(l=> l.Location)
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .FirstOrDefault(c => c.Id == id);

            // If the customer was not found then let the user know
            if (dbCustomer == null)
            {
                return null;
            }
            string result = "";
            // if one was found then map it to a usable object
            Domain.Models.Customer m_customer = Mapper_Customer.MapCustomerWithOrders(dbCustomer);

            return m_customer;
        }

        /// <summary>
        /// The purpose of this class is to fill the static Domain in the models with the book information
        /// </summary>
        public IEnumerable<Domain.Models.Book> FillBookLibrary()
        {
            IEnumerable<BookEntity> dbBooks = _context.Books.ToList();
            foreach (BookEntity b in dbBooks)
            {
                Domain.Models.Book.Library.Add(Mapper_Book.Map(b));
            }
            return dbBooks.Select(Mapper_Book.Map);
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
