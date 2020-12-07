using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class LocationController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ILogger _logger;

        public LocationController(ILogger<LocationController> logger, IStoreRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: LocationController
        public ActionResult Index(string message = null)
        {
            var viewModel = _repository.GetAllLocations().Select(l => new LocationViewModel
            {
                Id = l.ID,
                Name = l.LocationName
            });
            ViewData["LocationErrorMessage"] = message;
            return View(viewModel);
        }

        // GET: LocationController/Details/5
        public ActionResult Inventory(int id)
        {
            var model = _repository.GetStocksForLocation(id).Select(s => new StockViewModel
            {
                Book = s.Book.Title,
                Quantity = s.Quantity
            });

            if (model.ToList().Count == 0)
            {
                return RedirectToAction(nameof(Index), new { message = $"Could not find any inventory associated with Location ID: {id}" });
            }
            return View(model);
        }

        public ActionResult OrderHistory(int id)
        {
            var models = _repository.GetOrderHistoryByLocationID(id).Select(o => new OrderViewModel
            {
                OrderNumber = o.OrderNumber,
                TimeStamp = o.TimeStamp,
                TotalCost = o.Purchase.Sum(p => p.LineCost)
            });
            TempData["LocationID"] = id;
            return View(models);
        }
    }
}
