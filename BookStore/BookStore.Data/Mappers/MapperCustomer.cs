using System.Linq;

namespace BookStore.Data.Mappers
{
    public static class MapperCustomer
    {
        /// <summary>
        /// Turn a model customer with their location into a entity customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Entities.CustomerEntity MapCustomerWithLocation(Domain.Models.Customer customer)
        {
            return new Entities.CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.ID,
                Location = MapperLocation.Map(customer.MyStoreLocation)
            };
        }
        /// <summary>
        /// Turn an entity customer with their location into a model customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Domain.Models.Customer MapCustomerWithLocation(Entities.CustomerEntity customer)
        {
            return new Domain.Models.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ID = customer.Id,
                MyStoreLocation = MapperLocation.MapLocationsWithInventory(customer.Location),
                MyCart = customer.Shoppingcarts.Select(sc => new Domain.Models.ShoppingCart
                {
                    DateCreated = (System.DateTime)sc.CreateData,
                    ID = sc.CartId,
                    CartItems = sc.Cartitems.Select(ci => new Domain.Models.CartItem
                    {
                        ID = ci.ItemId,
                        Book = Domain.Models.Book.GetBookFromLibrary(ci.BookIsbn),
                        Quantity = ci.Quantity
                    })
                }).FirstOrDefault()
            };
        }

        /// <summary>
        /// Turn an entity customer with their orders into a customer model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Domain.Models.Customer MapCustomerWithOrders(Entities.CustomerEntity customer)
        {
            return new Domain.Models.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ID = customer.Id,
                MyStoreLocation = MapperLocation.Map(customer.Location),
                Orders = customer.Orders.Select(MapperOrder.MapOrderWithOrderLines)
            };
        }

        /// <summary>
        /// This turns a customer Model into a customer entity, by assigning each relavent property
        /// to a column in the customer table
        /// </summary>
        /// <param name="customer">The customer model.</param>
        /// <returns></returns>
        public static Entities.CustomerEntity Map(Domain.Models.Customer customer)
        {
            return new Entities.CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.ID,
                LocationId = customer.MyStoreLocation.ID
            };
        }

        /// <summary>
        /// This turns a customer Entity into a customer model.
        /// </summary>
        /// <param name="customer">The customer entity.</param>
        /// <returns></returns>
        public static Domain.Models.Customer Map(Entities.CustomerEntity customer)
        {
            return new Domain.Models.Customer
            {
                ID = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MyStoreLocation = MapperLocation.Map(customer.Location)
            };
        }

    }
}
