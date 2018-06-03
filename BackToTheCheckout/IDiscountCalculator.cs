using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public interface IDiscountCalculator
    {
        int CalculateTotalDiscount(int itemId, int itemQuantity);

        int CalculateTotalDiscount(IEnumerable<ProductItem> basket);
    }
}
