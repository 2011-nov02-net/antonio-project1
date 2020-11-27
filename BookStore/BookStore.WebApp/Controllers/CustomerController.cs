using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IStoreRepository _repository;

        public CustomerController(IStoreRepository repository)
        {
            _repository = repository;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = _repository.GetCustomers().Select(c => new CustomerViewModel
            {
                ID = c.ID,
                FirstName =c.FirstName,
                LastName = c.LastName,
                MyStoreLocation = c.MyStoreLocation.LocationName
            });
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            var locations = _repository.GetCustomers().Select(c => new CustomerViewModel
            {
                allLocations = _repository.GetAllLocations().Select(l => new LocationViewModel
                {
                    Id = l.ID,
                    Name = l.LocationName
                })
            }).First();

            ViewBag.Hello = "Hi";
            return View(locations);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newCustomer = new Domain.Models.Customer { 
                FirstName = collection["FirstName"],
                LastName = collection["LastName"],
                MyStoreLocation = new Domain.Models.Location { ID = Int32.Parse(collection["allLocations"]) }
                };
                _repository.AddACustomer(newCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
