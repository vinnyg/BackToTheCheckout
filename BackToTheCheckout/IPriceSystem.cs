using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public interface IPriceSystem
    {
        int CalculateTotalDiscount(int itemId, int itemQuantity);
    }
}
