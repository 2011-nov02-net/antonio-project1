using System.Collections.Generic;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class CustomerEntity
    {
        public CustomerEntity()
        {
            Orders = new HashSet<OrderEntity>();
            Shoppingcarts = new HashSet<ShoppingcartEntity>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? LocationId { get; set; }

        public virtual LocationEntity Location { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<ShoppingcartEntity> Shoppingcarts { get; set; }
    }
}
