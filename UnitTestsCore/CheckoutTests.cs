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
        }

        [TestMethod]
        public void Should_CorrectlyScanBasketItems()
        {
            // Arrange
            var item = new ProductItem { Id = 0, Price = 10 };

            var checkout = new Checkout(priceSystem);

            // Act
            checkout.Scan(item);

            // Assert
            checkout.BasketItems.Contains(item);
        }

        [TestMethod]
        public void Should_ScanItemList()
        {
            // Arrange
            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };
            var item3 = new ProductItem { Id = 2, Price = 5 };
            var item4 = new ProductItem { Id = 0, Price = 10 };

            var items = new List<ProductItem> { item1, item2, item3, item4 };

            var checkout = new Checkout(null);

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

            checkout.Scan(new List<ProductItem>());
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
            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };
            var item3 = new ProductItem { Id = 2, Price = 5 };

            var items = new List<ProductItem> { item1, item2, item3 };

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

            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };

            var items = new List<ProductItem> { item1, item2, item1, item2 };

            checkout.Scan(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_DiscountIsApplied()
        {
            // Arrange
            var priceSystemMock = new PriceSystemMock { ItemPrice = 20 , TotalDiscount = 20 };
            var checkout = new Checkout(priceSystemMock);

            var item = new ProductItem { Id = 0, Price = 10 };

            checkout.Scan(item);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void Should_CalculateCorrectTotalPrice_When_ItemsScannedIncrementally()
        {
            // Arrange
            var priceSystemMock = new PriceSystemMock();
            var checkout = new Checkout(priceSystemMock);

            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };
            var item3 = new ProductItem { Id = 2, Price = 5 };
            // Act
            // Assert
            var runningTotalPrice = 0;

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(10, runningTotalPrice);

            checkout.Scan(item2);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(15, runningTotalPrice);

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(25, runningTotalPrice);

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(15, runningTotalPrice);

        }

        private class PriceSystemMock : IPriceSystem
        {
            public int TotalDiscount { get; set; }
            public int ItemPrice { get; set; }

            public int CalculateTotalDiscount(BasketItem item)
            {
                return TotalDiscount;
            }

            public int CalculateTotalDiscount(int itemId, int itemQuantity)
            {
                return TotalDiscount;
            }

            public int GetPrice(int itemId)
            {

                return itemId;
                //return ItemPrice[itemId];
            }
        }
    }
}
