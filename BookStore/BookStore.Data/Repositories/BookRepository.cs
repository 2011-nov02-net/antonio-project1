using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Models;
using BookStore.Domain.Interfaces;

namespace BookStore.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly StoreContext _context;

        /// <summary>
        /// A repository managing data access for Store objects,
        /// using Entity Framework.
        /// </summary>
        public BookRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Book> GetLibrary()
        {
            return _context.Books.Include(g => g.Genre).Select(Mappers.MapperBook.Map);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.Select(g => new Genre { ID = g.Id, Name = g.Name });
        }

        public void AddBook(Book newBook)
        {
            var db_book = Mappers.MapperBook.Map(newBook);
            _context.Add(db_book);
            _context.SaveChanges();
        }
        public Book GetBook(string isbn) { return null; }
        public Book DeleteBook(Book bookToDelete) { return null; }
        public Book UpdateBook(Book bookToUpdate) { return null; }
    }
}
