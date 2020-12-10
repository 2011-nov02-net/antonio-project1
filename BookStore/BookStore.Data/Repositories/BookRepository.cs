using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Models;

namespace BookStore.Data.Repositories
{
    public class BookRepository
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
    }
}
