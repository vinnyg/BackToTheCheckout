using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class PriceSystem
    {
        private Dictionary<int, int> itemPrices;
        private Dictionary<int, Func<int, int>> discountRules;

        public PriceSystem(Dictionary<int, int> itemPrices, Dictionary<int, Func<int, int>> discountRules)
        {
            this.itemPrices = itemPrices;
            this.discountRules = discountRules;
        }

        public int GetPrice(int itemId)
        {
            return itemPrices[itemId];
        }

        public int CalculateTotalDiscount(BasketItem item)
        {
            return discountRules[item.Id].Invoke(item.Quantity);
        }
    }
}
