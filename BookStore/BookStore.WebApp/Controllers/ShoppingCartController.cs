﻿using BookStore.Domain.Interfaces;
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
        private readonly IStoreRepository _repository;

        public ShoppingCartController(IStoreRepository storerepo, ICartRepository repository)
        {
            _cartrepository = repository;
            _repository = storerepo;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            _repository.FillBookLibrary();
            var shoppingCart = _cartrepository.GetShoppingCartByCustomerID(Int32.Parse(TempData.Peek("CustomerID").ToString()));
            var cart = new ShoppingCartViewModel
            {
                CartItems = shoppingCart.CartItems.Select(ci => new CartItemViewModel
                {
                    Quantity = ci.Quantity,
                    ISBN = ci.Book.ISBN
                })
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(IFormCollection collection)
        {
            var customer = _repository.GetCustomers().Where(c => c.ID == Int32.Parse(TempData.Peek("CustomerID").ToString())).First();
            var book = Domain.Models.Book.GetBookFromLibrary(collection["isbn"]);
            int quantity = Int32.Parse(collection["qty"]);

            _cartrepository.RemoveCartItem(customer, book, quantity);

            TempData["TotalCartItems"] = customer.GetCartItemCount();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult PlaceOrder()
        {
            _repository.FillBookLibrary();
            Domain.Models.ShoppingCart shoppingCart = _cartrepository.GetShoppingCartByCustomerID(Int32.Parse(TempData.Peek("CustomerID").ToString()));
            Domain.Models.Order orderAttempt = new Domain.Models.Order();
            foreach (var item in shoppingCart.CartItems)
            {
                orderAttempt.AddNewOrderLine($"{item.Book.ISBN}, {item.Quantity}");
            }
            orderAttempt.CustomerPlaced = _repository.GetCustomers().Where(c => c.ID == Int32.Parse(TempData.Peek("CustomerID").ToString())).First();
            orderAttempt.LocationPlaced = orderAttempt.CustomerPlaced.MyStoreLocation;

            orderAttempt.LocationPlaced.AttemptOrderAtLocation(orderAttempt);

            _repository.PlaceAnOrderForACustomer(orderAttempt);
            _cartrepository.EmptyCart(orderAttempt.CustomerPlaced);
            TempData["TotalCartItems"] = orderAttempt.CustomerPlaced.GetCartItemCount();
            return RedirectToAction(nameof(Index));

        }
    }
}
