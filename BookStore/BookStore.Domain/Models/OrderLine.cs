using System;
using System.Linq;

namespace BookStore.Domain.Models
{
    public class OrderLine
    {
        public string BookISBN { get; set; }
        public int Quantity { get; set; }
        public decimal LineCost { get; set; }
        public override string ToString()
        {
            return $"ISBN: {BookISBN}\tQty:{Quantity}\t";
        }
    }
}