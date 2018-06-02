using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public interface IPriceSystem
    {
        int GetPrice(int itemId);

        int CalculateTotalDiscount(int itemId, int itemQuantity);
    }
}
