using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class PriceSystem : IPriceSystem
    {
        private Dictionary<int, Func<int, int>> discountRules;

        public PriceSystem(Dictionary<int, Func<int, int>> discountRules)
        {
            this.discountRules = discountRules;
        }

        public int CalculateTotalDiscount(int itemId, int itemQuantity)
        {
            return discountRules[itemId].Invoke(itemQuantity);
        }
    }
}
