using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
            _logger.LogInformation("The main page has been accessed");

            return View();
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
