using BookStore.Domain.Interfaces;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.WebApp.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ICartRepository _cartrepository;

        public LibraryController(IStoreRepository repository, ICartRepository cartRepository)
        {
            _repository = repository;
            _cartrepository = cartRepository;
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
        public ActionResult Details(string isbn)
        {
            var b = _repository.GetBook(isbn);
            BookViewModel book = new BookViewModel
            {
                ISBN = b.ISBN,
                AuthorFirstName = b.AuthorFirstName,
                AuthorLastName = b.AuthorLastName,
                Price = b.Price,
                Title = b.Title,
                LocationsWithStock = _repository.GetLocationsIfStocksExistForISBN(Int32.Parse(TempData.Peek("MyStoreID").ToString()), isbn)
            };

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = _repository.GetCustomers().Where(c => c.ID == Int32.Parse(TempData.Peek("CustomerID").ToString())).First();
                    var book = Domain.Models.Book.GetBookFromLibrary(collection["isbn"]);
                    int quantity = Int32.Parse(collection["qty"]);

                    _cartrepository.AddCartItem(customer, book, quantity);

                    TempData["TotalCartItems"] = customer.GetCartItemCount();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
