using BackToTheCheckout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace UnitTestsCore
{
    [TestClass]
    public class CheckoutTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void Should_CorrectlyScanBasketItems()
        {
            // Arrange
            var item = new ProductItem { Id = 0 };

            var checkout = new Checkout(null);

            // Act
            checkout.Scan(item);

            // Assert
            checkout.BasketItems.Contains(item);
        }

        [TestMethod]
        public void Should_ScanItemList()
        {
            // Arrange
            var item1 = new ProductItem { Id = 0 };
            var item2 = new ProductItem { Id = 1 };
            var item3 = new ProductItem { Id = 2 };
            var item4 = new ProductItem { Id = 0 };

            var items = new List<ProductItem> { item1, item2, item3, item4 };

            var checkout = new Checkout(null);

            // Act
            checkout.ScanBasket(items);

            // Assert
            checkout.BasketItems.All(items.Contains);
        }

        [TestMethod]
        public void Should_ReturnPriceOf0_When_BasketIsEmpty()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(
                It.IsAny<int>(), It.IsAny<int>()
                )).Returns(0);

            var checkout = new Checkout(priceSystemMock.Object);

            checkout.ScanBasket(new List<ProductItem>());
            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereIsOneOfEachItem()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(
                It.IsAny<int>(), It.IsAny<int>()
                )).Returns(0);

            var checkout = new Checkout(priceSystemMock.Object);

            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };
            var item3 = new ProductItem { Id = 2, Price = 5 };

            var items = new List<ProductItem> { item1, item2, item3 };

            checkout.ScanBasket(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(35, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_ThereAreMultipleOfEachItem()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(
                It.IsAny<int>(), It.IsAny<int>()
                )).Returns(0);

            var checkout = new Checkout(priceSystemMock.Object);

            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };

            var items = new List<ProductItem> { item1, item2, item1, item2 };

            checkout.ScanBasket(items);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void Should_CorrectlyCalculatePrice_When_DiscountIsApplied()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(
                It.IsAny<IEnumerable<ProductItem>>()
                )).Returns(20);

            var checkout = new Checkout(priceSystemMock.Object);

            var item = new ProductItem { Id = 0, Price = 10 };

            checkout.Scan(item);
            checkout.Scan(item);
            checkout.Scan(item);

            // Act
            var result = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Should_CalculateCorrectTotalPriceWithoutDiscounts_When_ItemsScannedIncrementally()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(It.IsAny<IEnumerable<ProductItem>>()));

            var checkout = new Checkout(priceSystemMock.Object);

            var item1 = new ProductItem { Id = 0, Price = 10 };
            var item2 = new ProductItem { Id = 1, Price = 20 };
            // Act
            // Assert
            var runningTotalPrice = 0;

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(10, runningTotalPrice);

            checkout.Scan(item2);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(30, runningTotalPrice);

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(40, runningTotalPrice);

            checkout.Scan(item1);
            runningTotalPrice = checkout.CalculateTotalPrice();
            Assert.AreEqual(50, runningTotalPrice);
        }

        [TestMethod]
        public void Should_ApplyDiscount_When_DiscountApplicable()
        {
            // Arrange
            var priceSystemMock = new Mock<IDiscountCalculator>();
            priceSystemMock.Setup(x => x.CalculateTotalDiscount(It.IsAny<IEnumerable<ProductItem>>()));

            var checkout = new Checkout(priceSystemMock.Object);

            var item = new ProductItem { Id = 0, Price = 3 };

            checkout.Scan(item);
            checkout.Scan(item);
            checkout.Scan(item);

            // Act
            checkout.CalculateTotalPrice();

            // Assert
            priceSystemMock.Verify(x => x.CalculateTotalDiscount(
                It.Is<IEnumerable<ProductItem>>(items => items.All(checkout.BasketItems.Contains))),
                Times.Once);
        }
    }
}
