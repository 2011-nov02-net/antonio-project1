using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class GenreEntity
    {
        public GenreEntity()
        {
            Books = new HashSet<BookEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookEntity> Books { get; set; }
    }
}
