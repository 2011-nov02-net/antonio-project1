using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Domain.Models
{
    /// <summary>
    /// This is just used to store a book object
    /// </summary>
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorFullName { get => $"{AuthorFirstName} {AuthorLastName}"; }
        public decimal Price { get; set; }

        public static List<Book> Library = new List<Book>();

        /// <summary>
        /// If it werent for this function then this class could almost be a struct.
        /// But this method returns true if the library contains a given isbn
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool CheckIfIsValidIsbn(string candidate)
        {
            return Library.Any(b => b.ISBN == candidate);
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN}\tTitle: {Title}\tAuthor: {AuthorFullName}\tPrice: {Price}";
        }
    }
}
