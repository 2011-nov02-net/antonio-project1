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
    public class ShoppingCartController : Controller
    {
        private readonly ICartRepository _cartrepository;

        public ShoppingCartController(ICartRepository repository)
        {
            _cartrepository = repository;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCart = _cartrepository.GetShoppingCartByCustomerID(Int32.Parse(TempData.Peek("CustomerID").ToString()));
            var cart = new ShoppingCartViewModel
            {
                CartItems = shoppingCart.CartItems.Select(ci => new CartItemViewModel {
                    Quantity = ci.Quantity,
                    ISBN = ci.Book.ISBN
                })
            };
            return View(shoppingCart);
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
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

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Edit/5
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

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Delete/5
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
