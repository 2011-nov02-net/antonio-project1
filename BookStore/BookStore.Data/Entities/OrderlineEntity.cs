#nullable disable

namespace BookStore.Data.Entities
{
    public partial class OrderlineEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string BookIsbn { get; set; }
        public int Quantity { get; set; }

        public virtual BookEntity BookIsbnNavigation { get; set; }
        public virtual OrderEntity Order { get; set; }
    }
}
