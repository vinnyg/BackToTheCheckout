using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class BasketItem
    {
        public int Id { get; private set; }

        public int Quantity { get; private set; }

        public BasketItem(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
