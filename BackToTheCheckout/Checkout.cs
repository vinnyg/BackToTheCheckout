using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private IPriceSystem priceSystem;
        
        public List<ProductItem> BasketItems { get; private set; }

        public Checkout(IPriceSystem priceSystem)
        {
            this.priceSystem = priceSystem;
            BasketItems = new List<ProductItem>();
        }

        public void Scan(ProductItem item)
        {
            BasketItems.Add(item);
        }

        public void Scan(List<ProductItem> items)
        {
            BasketItems.AddRange(items);
        }

        public int CalculateTotalPrice()
        {
            var price = 0;

            foreach (var item in BasketItems)
            {
                price += item.Price;
            }

            var totalDiscount = CalculateTotalDiscounts(BasketItems);

            return price - totalDiscount;
        }

        private int CalculateTotalDiscounts(List<ProductItem> basket)
        {
            var totalApplicableDiscount = 0;
            var itemQuantities = basket.GroupBy(x => x.Id);

            foreach (var item in itemQuantities)
            {
                totalApplicableDiscount += priceSystem.CalculateTotalDiscount(item.Key, item.Count());
            }

            return 0;
        }
    }
}
