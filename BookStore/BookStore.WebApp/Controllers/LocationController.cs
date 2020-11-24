using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    public class LocationController : Controller
    {
        private readonly IStoreRepository _repository;

        public LocationController(IStoreRepository repository)
        {
            _repository = repository;
        }

        // GET: LocationController
        public ActionResult Index()
        {
            var viewModel = _repository.GetAllLocations().Select(l => new LocationViewModel
            {
                Id = l.ID,
                Name = l.LocationName
            });
            return View(viewModel);
        }

        // GET: LocationController/Details/5
        public ActionResult Inventory(int id)
        {
            var model = _repository.GetStocksForLocation(id).Select(s => new StockViewModel {
            Book = s.Book.Title,
            Quantity = s.Quantity
            });
            return View(model);
        }

        public ActionResult OrderHistory(int id)
        {
            var models = _repository.GetOrderHistoryByLocationID(id).Select(o => new OrderViewModel
            {
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerPlaced.Name,
                TimeStamp = o.TimeStamp
            });
            return View(models);
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
