using BackToTheCheckout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestsCore
{
    [TestClass]
    public class PriceSystemTests
    {
        private Dictionary<int, Func<int, int>> discountRules;
        private Dictionary<int, int> itemPrices;

        [TestInitialize]
        public void TestInitialize()
        {
            itemPrices = new Dictionary<int, int>();
            itemPrices.Add(0, 10);
            itemPrices.Add(1, 5);
            itemPrices.Add(2, 20);

            discountRules = new Dictionary<int, Func<int, int>>();
            discountRules.Add(0, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 10 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(1, (int quantity) => {
                var timesToApplyDiscount = quantity / 2;
                var totalDiscountAmount = 5 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(2, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 20 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
        }

        [TestMethod]
        public void Should_ReturnCorrectPriceForItem()
        {
            // Arrange
            var item = new BasketItem(0, 1);

            // Act
            var result = itemPrices[item.Id];

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Should_ReturnCorrectDiscountForItem()
        {
            // Arrange
            var itemId = 0;
            var itemQuantity = 3;

            var priceSystem = new PriceSystem(itemPrices, discountRules);
            // Act
            var totalDiscountAmount = priceSystem.CalculateTotalDiscount(itemId, itemQuantity);

            // Assert
            Assert.AreEqual(10, totalDiscountAmount);
        }

        [TestMethod]
        public void Should_ReturnCorrectDiscountTotal_When_DiscountAppliesMoreThanOnce()
        {
            // Arrange
            var itemId = 0;
            var itemQuantity = 6;

            var priceSystem = new PriceSystem(itemPrices, discountRules);

            // Act
            var totalDiscountAmount = priceSystem.CalculateTotalDiscount(itemId, itemQuantity);

            // Assert
            Assert.AreEqual(20, totalDiscountAmount);
        }
    }
}
