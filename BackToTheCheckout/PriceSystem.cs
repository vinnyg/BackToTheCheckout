using System;
using System.Collections.Generic;
using System.Linq;
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

        public int CalculateTotalDiscount(IEnumerable<ProductItem> basket)
        {
            var totalDiscount = 0;
            var groupedItems = basket.GroupBy(x => x.Id);

            foreach (var item in groupedItems)
            {
                totalDiscount += discountRules[item.Key].Invoke(item.Count());
            }

            return totalDiscount;
        }
    }
}
