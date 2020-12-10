using System.Collections.Generic;
using System.Linq;

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
        public string Imagelink { get; set; }
        public Genre Genre { get; set; }

        public static IEnumerable<Book> Library;

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

        public static Book GetBookFromLibrary(string isbn)
        {
            return Library.First(b=>b.ISBN==isbn);
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN}\tTitle: {Title}\tAuthor: {AuthorFullName}\tPrice: {Price}";
        }
    }
}
