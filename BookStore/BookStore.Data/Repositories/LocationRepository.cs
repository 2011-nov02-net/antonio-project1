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
    public class LocationRepository : ILocationRepository
    {
        private readonly StoreContext _context;

        /// <summary>
        /// A repository managing data access for Store objects,
        /// using Entity Framework.
        /// </summary>
        public LocationRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Location AddLocation(Location newLocation)
        {
            throw new NotImplementedException();
        }

        public void AdjustStockForLocation(int locationID, string ISBN, int newQuantity)
        {
            var location = _context.Locations.Include(i => i.Inventories).First(l=>l.Id == locationID);
            var stock = location.Inventories.First(i => i.BookIsbn == ISBN);
            stock.Quantity = newQuantity;

            _context.SaveChanges();
        }

        public IEnumerable<Location> GetAllLocations(string search = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderHistoryByLocationID(int locationID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetStocksForLocation(int locationID)
        {
            throw new NotImplementedException();
        }
    }
}
