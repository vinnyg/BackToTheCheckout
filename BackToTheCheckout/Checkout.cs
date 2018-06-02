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
            var price = BasketItems.Sum(item => item.Price);

            var totalDiscount = priceSystem.CalculateTotalDiscount(BasketItems);

            return price - totalDiscount;
        }
    }
}
