using System;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class CartitemEntity
    {
        public int ItemId { get; set; }
        public string BookIsbn { get; set; }
        public int Quantity { get; set; }
        public DateTime? DataAdded { get; set; }
        public int? ShoppingcartId { get; set; }

        public virtual BookEntity BookIsbnNavigation { get; set; }
        public virtual ShoppingcartEntity Shoppingcart { get; set; }
    }
}
