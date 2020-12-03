using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IStoreRepository _repository;

        public OrderController(IStoreRepository repository)
        {
            _repository = repository;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var orderDetails = _repository.GetDetailsForOrder(id);
            var vm_orderDetails = new OrderViewModel
            {
                TimeStamp = orderDetails.TimeStamp,
                CustomerName = orderDetails.CustomerPlaced.Name,
                LocationName = orderDetails.LocationPlaced.LocationName,
                OrderNumber = orderDetails.OrderNumber,
                TotalCost = orderDetails.GetOrderTotal(),
                Purchase = orderDetails.Purchase.Select(ol => new OrderLineViewModel
                {
                    BookISBN = ol.BookISBN,
                    LineCost = ol.LineCost,
                    Quantity = ol.Quantity
                }).ToList()
            };

            return View(vm_orderDetails);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
