using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IStoreRepository _repository;

        public LibraryController(IStoreRepository repository)
        {
            _repository = repository;
        }

        // GET: LibraryController
        public ActionResult Index()
        {
            var library = _repository.FillBookLibrary().Select(b => new BookViewModel
            {
                ISBN = b.ISBN,
                AuthorFirstName = b.AuthorFirstName,
                AuthorLastName = b.AuthorLastName,
                Price = b.Price,
                Title = b.Title
            });
            return View(library);
        }

        // GET: LibraryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibraryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibraryController/Create
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

        // GET: LibraryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LibraryController/Edit/5
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

        // GET: LibraryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibraryController/Delete/5
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
