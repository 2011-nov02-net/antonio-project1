using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository _repository;

        public HomeController(ILogger<HomeController> logger, IStoreRepository storeRepository)
        {
            _logger = logger;
            _repository = storeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomerList(int id)
        {
            var customers = _repository.GetCustomers()
                .Where(a => a.ID == id)
                        .Select(x => new SelectListItem
                        {
                            Value = x.ID.ToString(),
                            Text = x.FirstName
                        }).ToList();
            return Json(customers, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetLocationList()
        {
            return _repository.GetAllLocations()
                .Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.LocationName
            }).ToList();
        }

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
                return View(collection);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
