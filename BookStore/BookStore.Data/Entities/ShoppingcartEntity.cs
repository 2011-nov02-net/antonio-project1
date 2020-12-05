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
            Customers = new HashSet<CustomerEntity>();
        }

        public int CartId { get; set; }
        public DateTime? CreateData { get; set; }

        public virtual ICollection<CartitemEntity> Cartitems { get; set; }
        public virtual ICollection<CustomerEntity> Customers { get; set; }
    }
}
