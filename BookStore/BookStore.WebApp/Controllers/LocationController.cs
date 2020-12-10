using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class LocationController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ILocationRepository _locationrepository;

        public LocationController(IStoreRepository repository, ILocationRepository locationRepository)
        {
            _repository = repository;
            _locationrepository = locationRepository;
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
                Quantity = s.Quantity,
                CurrentLocationID = id,
                ISBN = s.Book.ISBN
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


        public ActionResult Edit(int locationID, int currentStock, string isbn, string book)
        {
            var stock = new StockViewModel
            {
                CurrentLocationID = locationID,
                ISBN = isbn,
                Quantity = currentStock,
                Book = book
            };
            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                int lid = Int32.Parse(collection["CurrentLocationID"].ToString());
                _locationrepository.AdjustStockForLocation(
                    lid,
                    collection["ISBN"].ToString(),
                    Int32.Parse(collection["Quantity"].ToString())
                    );
                return RedirectToAction(nameof(Inventory), new {id = lid});
            }
            catch (Exception) 
            { 
                return RedirectToAction(nameof(Index)); 
            }
        }
    }
}