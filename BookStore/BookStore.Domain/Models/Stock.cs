namespace BookStore.Domain.Models
{
    public class Stock
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }

        public void AdjustStock(int amount)
        {
            if (Quantity - amount < 0)
            {
                Quantity = 0;
            }
            else
            {
                Quantity -= amount;
            }
        }

        public bool CheckStock(int check)
        {
            if (Quantity <= 0)
            {
                return false;
            }
            if ((Quantity + check) < 0)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"BookISBN: {Book} \tQty: {Quantity}";
        }
    }
}
