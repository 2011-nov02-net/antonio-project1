using System.Linq;

namespace BookStore.Data.Mappers
{
    public static class Mapper_Location
    {
        /// <summary>
        /// This turns a Location Model into a Location entity, by assigning each relavent property
        /// to a column in the Location table
        /// </summary>
        /// <param name="location">The location model.</param>
        /// <returns new Entity.Location></returns>
        public static Entities.LocationEntity MapLocationWithOrders(Domain.Models.Location location)
        {
            return new Entities.LocationEntity
            {
                Id = location.ID,
                Name = location.LocationName,
                Orders = location.OrderHistory.Select(Mapper_Order.Map).ToList()
            };
        }

        /// <summary>
        /// This turns a Location Entity into a Location model.
        /// </summary>
        /// <param name="customer">The customer entity.</param>
        /// <returns></returns>
        public static Domain.Models.Location MapLocationWithOrders(Entities.LocationEntity location)
        {
            return new Domain.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                OrderHistory = location.Orders.Select(Mapper_Order.MapOrderWithOrderLines).ToList()
            };
        }

        /// <summary>
        /// turn an entity location with their inventory into a model inventory
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Domain.Models.Location MapLocationsWithInventory(Entities.LocationEntity location)
        {
            return new Domain.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                Inventory = location.Inventories.Select(Mapper_Inventory.Map).ToList()
            };
        }

        /// <summary>
        /// Turn a location into an entity location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Entities.LocationEntity Map(Domain.Models.Location location)
        {
            return new Entities.LocationEntity
            {
                Id = location.ID,
                Name = location.LocationName
            };
        }

        /// <summary>
        /// Turn a location into an entity location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Domain.Models.Location Map(Entities.LocationEntity location)
        {
            return new Domain.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name
            };
        }
    }
}
