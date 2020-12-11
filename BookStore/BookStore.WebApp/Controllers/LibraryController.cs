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
        private readonly IBookRepository _bookrepository;

        public LibraryController(IStoreRepository repository, ICartRepository cartRepository, IBookRepository bookRepository)
        {
            _repository = repository;
            _cartrepository = cartRepository;
            _bookrepository = bookRepository;
        }

        // GET: LibraryController
        public ActionResult Index(string authorNameSearch = null,
            string titleSearch = null,
            string isbnSearch = null,
            string genreSearch = null)
        {
            var library = _repository.FillBookLibrary().Select(b => new BookViewModel
            {
                ISBN = b.ISBN,
                AuthorFirstName = b.AuthorFirstName,
                AuthorLastName = b.AuthorLastName,
                Price = b.Price,
                Title = b.Title,
                ImageLink = b.Imagelink,
                Genre = b.Genre.Name
            });

            if (!string.IsNullOrEmpty(authorNameSearch))
            {
                library = library.Where(l => l.AuthorFullName.Contains(authorNameSearch));
            }
            if (!string.IsNullOrEmpty(titleSearch))
            {
                library = library.Where(l => l.Title.Contains(titleSearch));
            }
            if (!string.IsNullOrEmpty(isbnSearch))
            {
                library = library.Where(l => l.ISBN.Contains(isbnSearch));
            }
            if (!string.IsNullOrEmpty(genreSearch))
            {
                library = library.Where(l => l.Genre.Contains(genreSearch));
            }

            return View(library);
        }

        // GET: LibraryController/Details/5
        public ActionResult Details(string isbn)
        {
            var b = _repository.FillBookLibrary(isbn).First();
            BookViewModel book = new BookViewModel
            {
                ISBN = b.ISBN,
                AuthorFirstName = b.AuthorFirstName,
                AuthorLastName = b.AuthorLastName,
                Price = b.Price,
                Title = b.Title,
                Genre = b.Genre.Name,
                LocationsWithStock = _repository.GetLocationsIfStocksExistForISBN(Int32.Parse(TempData.Peek("MyStoreID").ToString()), isbn),
                ImageLink = b.Imagelink
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
                    _repository.FillBookLibrary();
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

        public ActionResult Create(BookViewModel model = null)
        {
            var newBook = new BookViewModel();
            if (model != null) 
            {
                newBook = model;
            }
            newBook.GenreList = _bookrepository.GetGenres().Select(g => new GenreViewModel
            {
                ID = g.ID,
                Name = g.Name
            });
            return View(newBook);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var newBook = new Domain.Models.Book();
            if (ModelState.IsValid) {
                newBook.Title = collection["Title"];
                newBook.AuthorFirstName = collection["AuthorFirstName"];
                newBook.AuthorLastName = collection["AuthorLastName"];
                newBook.Imagelink = collection["ImageLink"];
                newBook.ISBN = collection["ISBN"];
                newBook.Price = decimal.Parse(collection["Price"]);
                newBook.Genre = new Domain.Models.Genre { ID = Int32.Parse(collection["GenreList"]) };
            }
            try
            {
                _bookrepository.AddBook(newBook);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Create), newBook);
            }
        }
    }
}
