using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class OrderEntity
    {
        public OrderEntity()
        {
            Orderlines = new HashSet<OrderlineEntity>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual LocationEntity Location { get; set; }
        public virtual ICollection<OrderlineEntity> Orderlines { get; set; }
    }
}
