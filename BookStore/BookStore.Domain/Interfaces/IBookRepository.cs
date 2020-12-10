using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetLibrary();
        void AddBook(Book newBook);
        Book GetBook(string isbn);
        Book DeleteBook(Book bookToDelete);
        Book UpdateBook(Book bookToUpdate);
        IEnumerable<Genre> GetGenres();
    }
}
