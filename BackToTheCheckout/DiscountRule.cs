using System;
using System.Collections.Generic;
using System.Text;

namespace BackToTheCheckout
{
    public class DiscountRule
    {
        public List<int> ApplicableProducts { get; set; }

        public int DiscountAmount { get; set; }
    }
}