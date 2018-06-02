using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class PriceSystem : IPriceSystem
    {
        private Dictionary<int, int> itemPrices;
        private Dictionary<int, Func<int, int>> discountRules;

        public PriceSystem(Dictionary<int, int> itemPrices, Dictionary<int, Func<int, int>> discountRules)
        {
            this.itemPrices = itemPrices;
            this.discountRules = discountRules;
        }

        public int CalculateTotalDiscount(int itemId, int itemQuantity)
        {
            return discountRules[itemId].Invoke(itemQuantity);
        }
    }
}
