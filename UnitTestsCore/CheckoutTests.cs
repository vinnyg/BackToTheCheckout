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
        private int[] pricingIndex;

        [TestInitialize]
        public void TestInitialize()
        {
            pricingIndex = new int[] { 10, 5, 20 };
        }

        [TestMethod]
        public void Should_CorrectlyScanBasketItems()
        {
            // Arrange
            var item1 = new BasketItem(0, 1);
            var item2 = new BasketItem(1, 2);

            var items = new List<BasketItem> { item1, item2 };

            var checkout = new Checkout(new int[0]);

            // Act
            checkout.Scan(items);

            // Assert
            checkout.BasketItems.All(items.Contains);
        }

        [TestMethod]
        public void Should_ReturnPriceOf0_When_BasketIsEmpty()
        {
            // Arrange
            var checkout = new Checkout(pricingIndex);

            checkout.Scan(new List<BasketItem>());
            // Act
            var result = checkout.CalculatePrice();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereIsOneOfEachItem()
        {
            // Arrange
            var checkout = new Checkout(pricingIndex);
            var item1 = new BasketItem(0, 1);
            var item2 = new BasketItem(1, 1);
            var item3 = new BasketItem(2, 1);

            var items = new List<BasketItem> { item1, item2, item3 };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculatePrice();

            // Assert
            Assert.AreEqual(35, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereAreMultipleOfEachItem()
        {
            // Arrange
            var checkout = new Checkout(pricingIndex);

            var item1 = new BasketItem(0, 3);
            var item2 = new BasketItem(1, 2);

            var items = new List<BasketItem> { item1, item2 };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculatePrice();

            // Assert
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void SHould_CorrectlyCalculatePrice_When_DiscountIsApplied()
        {
            // Arrange
            var checkout = new Checkout(pricingIndex);
        }
    }
}
