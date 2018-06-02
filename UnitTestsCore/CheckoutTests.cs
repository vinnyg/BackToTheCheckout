using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackToTheCheckout;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestsCore
{
    [TestClass]
    public class CheckoutTests
    {
        private PriceSystem priceSystem;

        [TestInitialize]
        public void TestInitialize()
        {
            var itemPrices = new Dictionary<int, int>();
            itemPrices.Add(0, 10);
            itemPrices.Add(1, 5);
            itemPrices.Add(2, 20);

            var discountRules = new Dictionary<int, Func<int, int>>();
            discountRules.Add(0, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 10 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(1, (int quantity) => {
                var timesToApplyDiscount = quantity / 4;
                var totalDiscountAmount = 5 * timesToApplyDiscount;
                return totalDiscountAmount;
            });
            discountRules.Add(2, (int quantity) => {
                var timesToApplyDiscount = quantity / 3;
                var totalDiscountAmount = 20 * timesToApplyDiscount;
                return totalDiscountAmount;
            });


            priceSystem = new PriceSystem(itemPrices, discountRules);
        }

        [TestMethod]
        public void Should_CorrectlyScanBasketItems()
        {
            // Arrange

            var item1 = new BasketItem(0, 1);
            var item2 = new BasketItem(1, 2);

            var items = new List<BasketItem> { item1, item2 };

            var checkout = new Checkout(priceSystem);

            // Act
            checkout.Scan(items);

            // Assert
            checkout.BasketItems.All(items.Contains);
        }

        [TestMethod]
        public void Should_ReturnPriceOf0_When_BasketIsEmpty()
        {
            // Arrange
            var checkout = new Checkout(priceSystem);

            checkout.Scan(new List<BasketItem>());
            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereIsOneOfEachItem()
        {
            // Arrange
            var checkout = new Checkout(priceSystem);
            var item1 = new BasketItem(0, 1);
            var item2 = new BasketItem(1, 1);
            var item3 = new BasketItem(2, 1);

            var items = new List<BasketItem> { item1, item2, item3 };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(35, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereAreMultipleOfEachItem()
        {
            // Arrange
            var checkout = new Checkout(priceSystem);

            var item1 = new BasketItem(0, 2);
            var item2 = new BasketItem(1, 2);

            var items = new List<BasketItem> { item1, item2 };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_DiscountIsApplied()
        {
            // Arrange
            var checkout = new Checkout(priceSystem);

            var item = new BasketItem(0, 3);

            var items = new List<BasketItem> { item };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(20, result);
        }
    }
}
