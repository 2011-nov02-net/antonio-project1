using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
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
        public ActionResult Details(int id, bool justPlaced = false)
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
            if (justPlaced)
            {
                TempData["NewOrder"] = "Order Confirmed!";
            }
            return View(vm_orderDetails);
        }
    }
}
