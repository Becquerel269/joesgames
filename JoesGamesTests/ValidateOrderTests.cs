using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JoesGamesTests
{
    [TestClass]
    public class ValidateOrderTests
    {
        private ValidateOrderService _validateOrderService;
        private IOrder validOrder;
        private IOrder invalidOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            _validateOrderService = new ValidateOrderService();
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
            var result = _validateOrderService.IsOrderValid(validOrder);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOrderValid_ReturnsFalse_WithNotValidOrder()
        {
            // Arrange
            

            // Act
            var result = _validateOrderService.IsOrderValid(invalidOrder);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOrderValid_ReturnsNull_WithNull()
        {
            // Act
            bool result = _validateOrderService.IsOrderValid(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}