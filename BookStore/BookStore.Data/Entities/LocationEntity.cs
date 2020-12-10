using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class LocationEntity
    {
        public LocationEntity()
        {
            Customers = new HashSet<CustomerEntity>();
            Inventories = new HashSet<InventoryEntity>();
            Orders = new HashSet<OrderEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }
        public virtual ICollection<InventoryEntity> Inventories { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
