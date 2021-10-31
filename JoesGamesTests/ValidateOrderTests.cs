using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JoesGamesTests
{
    [TestClass]
    public class ValidateOrderTests
    {
        private ValidateOrder validateOrder;
        private IOrder validOrder;
        private IOrder invalidOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            validateOrder = new ValidateOrder();
            validOrder = new Order()
            {
                Name = "validname",
                Id = "1",
                Items = Array.Empty<string>()
            };
            invalidOrder = new Order
            {
                Name = null,
                Id = null,
                Items = new string[]
                {
                }
            };
        }

        [TestMethod]
        public void IsOrderValid_ReturnsTrue_WithValidOrder()
        {
            // Arrange
            

            // Act
            var result = validateOrder.IsOrderValid(validOrder);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOrderValid_ReturnsFalse_WithNotValidOrder()
        {
            // Arrange
            

            // Act
            var result = validateOrder.IsOrderValid(invalidOrder);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOrderValid_ReturnsNull_WithNull()
        {
            // Act
            bool result = validateOrder.IsOrderValid(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}