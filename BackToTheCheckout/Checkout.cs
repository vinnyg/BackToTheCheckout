using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private int[] pricingIndex;
        private PriceSystem priceSystem;

        public List<BasketItem> BasketItems { get; private set; }

        public Checkout(PriceSystem priceSystem)
        {
            this.priceSystem = priceSystem;
        }

        public void Scan(List<BasketItem> items)
        {
            this.BasketItems = items;
        }

        public int CalculateTotalPrice()
        {
            var price = 0;

            foreach (var item in BasketItems)
            {
                var itemTotalPrice = item.Quantity * priceSystem.GetPrice(item.Id);
                var itemDiscount = priceSystem.CalculateTotalDiscount(item);

                price = price + itemTotalPrice - itemDiscount;
            }

            return price;
        }
    }
}
