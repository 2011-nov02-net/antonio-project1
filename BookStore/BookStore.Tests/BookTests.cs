
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
            Book.Library = new[] {new Book
            {
                ISBN = "111"
            } };

            Assert.True(Book.CheckIfIsValidIsbn("111"), "The book should exist!");
        }

        [Fact]
        public void CantFindABookThatDoesntExist()
        {
            Assert.False(Book.CheckIfIsValidIsbn("0141"), "The book should not exist!");
        }
    }
}
