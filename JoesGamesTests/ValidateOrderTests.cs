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
        private IOrderDTO validOrderDto;
        private IOrderDTO invalidOrderDto;

        [TestInitialize]
        public void TestInitialize()
        {
            _validateOrderService = new ValidateOrderService();
            validOrderDto = new OrderDTO()
            {
                Name = "validname",
                Id = "1"
                
            };
            invalidOrderDto = new OrderDTO()
            {
                Name = null,
                Id = null
               
            };
        }

        [TestMethod]
        public void IsOrderValid_ReturnsTrue_WithValidOrder()
        {
            // Arrange
            

            // Act
            var result = _validateOrderService.IsOrderValid(validOrderDto);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOrderValid_ReturnsFalse_WithNotValidOrder()
        {
            // Arrange
            

            // Act
            var result = _validateOrderService.IsOrderValid(invalidOrderDto);

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