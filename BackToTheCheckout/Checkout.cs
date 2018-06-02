using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private int[] pricingIndex;

        public List<BasketItem> BasketItems { get; private set; }

        public Checkout(int[] pricingIndex)
        {
            this.pricingIndex = pricingIndex;
        }

        public void Scan(List<BasketItem> items)
        {
            this.BasketItems = items;
        }

        public int CalculatePrice()
        {
            var price = 0;

            foreach (var item in BasketItems)
            {
                price += item.Quantity * pricingIndex[item.Id];
            }

            return price;
        }
    }
}
