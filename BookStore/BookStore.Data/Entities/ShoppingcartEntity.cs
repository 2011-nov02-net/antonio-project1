using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class ShoppingcartEntity
    {
        public ShoppingcartEntity()
        {
            Cartitems = new HashSet<CartitemEntity>();
        }

        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CreateData { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual ICollection<CartitemEntity> Cartitems { get; set; }
    }
}
