using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
        public ActionResult Index(string searchString)
        {
            var customers = _repository.GetCustomers().Select(c => new CustomerViewModel
            {
                ID = c.ID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MyStoreLocation = c.MyStoreLocation.LocationName
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Name.Contains(searchString));
            }

            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var customerDetails = _repository.GetOrderHistoryByCustomer(id);
            var customer = new CustomerViewModel
            {
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName,
                ID = customerDetails.ID,
                MyStoreLocation = customerDetails.MyStoreLocation.LocationName,
                Orders = customerDetails.Orders.Select(o => new OrderViewModel
                {
                    OrderNumber = o.OrderNumber,
                    TotalCost = o.GetOrderTotal(),
                    TimeStamp = o.TimeStamp
                }
              )
            };
            return View(customer);
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
            return View(locations);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
                var newCustomer = new Domain.Models.Customer
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    MyStoreLocation = new Domain.Models.Location { ID = Int32.Parse(collection["allLocations"]) }
                };
                _repository.AddACustomer(newCustomer);
                return RedirectToAction(nameof(Index));
            
        }

        public ActionResult AssignCurrentCustomer(int id)
        {
            var customer = _repository.GetCustomers().Where(c => c.ID == id).First();
            TempData["CustomerID"] = id;
            TempData["CustomerName"] = customer.Name;
            TempData["MyStoreID"] = customer.MyStoreLocation.ID;
            TempData["TotalCartItems"] = customer.GetCartItemCount();
            return RedirectToAction(nameof(Index));
        }
    }
}
