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

        [TestInitialize]
        public void TestInitialize()
        {
            discountRules = new Dictionary<int, Func<int, int>>();
            discountRules.Add(0, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 10 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(1, (int quantity) => {
                var timesToApplyDiscount = quantity / 2;
                var totalDiscountAmount = 15 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(2, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 8 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
        }

        [TestMethod]
        public void Should_ReturnCorrectDiscountForItem()
        {
            // Arrange
            var itemId = 0;
            var itemQuantity = 3;

            var priceSystem = new DiscountCalculator(discountRules);
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

            var priceSystem = new DiscountCalculator(discountRules);

            // Act
            var totalDiscountAmount = priceSystem.CalculateTotalDiscount(itemId, itemQuantity);

            // Assert
            Assert.AreEqual(20, totalDiscountAmount);
        }

        [TestMethod]
        public void Should_CalculateCorrectDiscountForAllItems_When_ManyDiscountsApply()
        {
            // Arrange
            var items = new List<ProductItem>
            {
                new ProductItem { Id = 0, Price = 10 },
                new ProductItem { Id = 0, Price = 10 },
                new ProductItem { Id = 1, Price = 20 },
                new ProductItem { Id = 1, Price = 20 },
                new ProductItem { Id = 0, Price = 10 },
                new ProductItem { Id = 0, Price = 10 },
                new ProductItem { Id = 1, Price = 20 },
                new ProductItem { Id = 2, Price = 5 },
                new ProductItem { Id = 0, Price = 10 },
                new ProductItem { Id = 0, Price = 10 }
            };

            var priceSystem = new DiscountCalculator(discountRules);
            // Act
            var totalDiscountPrice = priceSystem.CalculateTotalDiscount(items);

            // Assert
            Assert.AreEqual(35, totalDiscountPrice);
        }

        [TestMethod]
        public void Should_ReturnCorrectDiscount_When_ApplyingMultiProductDiscounts()
        {
            // Arrange
            var itemId = 0;

            var items = new List<ProductItem>
            {
                new ProductItem { Id = 4, Price = 10 },
                new ProductItem { Id = 4, Price = 10 },
                new ProductItem { Id = 5, Price = 20 }
            };

            // Act
            var totalDiscount = 0;

            // Assert
            Assert.AreEqual(30, totalDiscount);
        }
    }
}
