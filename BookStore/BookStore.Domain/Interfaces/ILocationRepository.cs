using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations(string search = null);
        Location AddLocation(Location newLocation);
        IEnumerable<Order> GetOrderHistoryByLocationID(int locationID);
        IEnumerable<Stock> GetStocksForLocation(int locationID);


    }
}
