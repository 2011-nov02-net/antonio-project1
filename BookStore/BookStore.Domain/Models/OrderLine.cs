using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Models
{
    public class OrderLine
    {
        public string BookISBN { get; set; }
        public int Quantity { get; set; }

        private decimal _lineCost = 0;
        public decimal LineCost
        {
            get { return _lineCost; }
            set { _lineCost = Convert.ToDecimal(Quantity * Book.Library.Find(b => b.ISBN == BookISBN).Price); }
        }

        public override string ToString()
        {
            return $"ISBN: {BookISBN}\tQty:{Quantity}\t";
        }
    }
}