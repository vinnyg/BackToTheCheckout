using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private IDiscountCalculator discountCalculator;
        
        public List<ProductItem> BasketItems { get; private set; }

        public Checkout(IDiscountCalculator discountCalculator)
        {
            this.discountCalculator = discountCalculator;
            BasketItems = new List<ProductItem>();
        }

        public void Scan(ProductItem item)
        {
            BasketItems.Add(item);
        }

        public void ScanBasket(List<ProductItem> items)
        {
            BasketItems.AddRange(items);
        }

        public int CalculateTotalPrice()
        {
            var price = BasketItems.Sum(item => item.Price);

            var totalDiscount = discountCalculator.CalculateTotalDiscount(BasketItems);

            return price - totalDiscount;
        }
    }
}
