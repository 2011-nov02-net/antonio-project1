using System.Collections.Generic;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public List<Stock> Inventory { get; set; } = new List<Stock>();

        /// <summary>
        /// Locations don't want to allow orders to occur if their stocks don't have enough product
        /// The purpose of this function is to ensure that the stocks have enough books to place an order.
        /// </summary>
        /// <param name="newOrder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AttemptOrderAtLocation(Order newOrder)
        {
            int attempted = 0;

            // We check each orderline that exists to get the isbns that were in the order
            foreach (OrderLine ol in newOrder.Purchase)
            {
                // Check each one if the book is even in the library and if there is enough
                if (CheckStockForOrderAttempt(Book.Library.First(b => b.ISBN.Contains(ol.BookISBN)), ol.Quantity))
                {
                    attempted++;
                }
            }

            // Just a double check that the amount books checked equal to the books that were attempted to place
            if (newOrder.Purchase.ToList().Count == attempted)
            {
                foreach (OrderLine ol in newOrder.Purchase)
                {
                    // Since we made it this far we can assume that the stocks exist for the books we want to purchase so we adjust the stocks
                    Inventory.Find(b => b.Book.ISBN == ol.BookISBN).AdjustStock(ol.Quantity);
                }

                // We also add this order to this locations order history
                OrderHistory.Add(newOrder);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks a locations stock if a books stock has enough to place an order
        /// </summary>
        /// <param name="book"></param>
        /// <param name="amount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckStockForOrderAttempt(Book book, int amount)
        {
            foreach (Stock i in Inventory)
            {
                if (i.Book.ISBN == book.ISBN)
                {
                    if (i.CheckStock(amount))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"ID: {ID}\tName: {LocationName}";
        }
    }
}
