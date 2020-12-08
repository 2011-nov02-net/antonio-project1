
using BookStore.Domain.Models;
using Xunit;
using System.Linq;

namespace BookStore.Tests
{
    public class BookTests
    {
        [Fact]
        public void CanFindABookThatExists()
        {
            var newBook = new Book
            {
                ISBN = "111"
            };
            Book.Library.ToList().Add(newBook);

            Assert.True(Book.CheckIfIsValidIsbn("111"), "The book should exist!");
        }

        [Fact]
        public void CantFindABookThatDoesntExist()
        {
            Assert.False(Book.CheckIfIsValidIsbn("0141"), "The book should not exist!");
        }
    }
}
