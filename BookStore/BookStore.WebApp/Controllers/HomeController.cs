using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository _storerepository;

        public HomeController(ILogger<HomeController> logger, IStoreRepository storeRepository)
        {
            _logger = logger;
            _storerepository = storeRepository;
        }

        public IActionResult Index()
        {
            var locaSales = _storerepository.GetLocationNamesWithTotalSales();
            var data = new HomeViewModel
            {
                LocationSales = locaSales,
                LocationWithMostSales = locaSales.Aggregate((x, y) => x.Value > y.Value ? x : y).Key
        };

            return View(data);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("The privacy page has been accessed");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
