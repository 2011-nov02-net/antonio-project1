namespace BookStore.Data.Mappers
{
    public static class Mapper_Book
    {
        /// <summary>
        /// The purpose of this method is to take a db object and turn it into a model object
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static Domain.Models.Book Map(Entities.BookEntity book)
        {
            return new Domain.Models.Book
            {
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                ISBN = book.Isbn,
                Price = book.Price,
                Title = book.Name,
            };
        }

        /// <summary>
        /// The purpose of this method to take a model book and turn it into a db book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static Entities.BookEntity Map(Domain.Models.Book book)
        {
            return new Entities.BookEntity
            {
                Isbn = book.ISBN,
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                Name = book.ISBN,
                Price = book.Price
            };
        }
    }
}
