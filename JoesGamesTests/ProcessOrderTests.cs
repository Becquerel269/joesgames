using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;

namespace JoesGamesTests.ProcessOrderTests
{
    [TestClass]
    public class ProcessOrderTests
    {
        private Order validOrder;
        private Order invalidOrder;
        private IProcessOrderService _processOrderService;
        private IValidateOrderService _validateOrderService;
        private IOrderData orderData;
        private IFileProcessService fileProcessService;

        [TestInitialize]
        public void TestInitialize()
        {
            _validateOrderService = new ValidateOrderService();
            orderData = new OrderData();
            _processOrderService = new ProcessOrderService(_validateOrderService, orderData, fileProcessService);
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
        public void ProcessOrder_ReturnsTrue_WithValidOrder()
        {
            // Arrange
            

            // Act
            var result = _processOrderService.SubmitOrder(validOrder);

            // Assert
            Assert.AreEqual(JsonSerializer.Serialize(validOrder), JsonSerializer.Serialize(result));
        }

        [TestMethod]
        
        public void ProcessOrder_ReturnsFalse_WhenOrderNameNotSupplied()
        {
            // Arrange
           

            // Act
            var result = _processOrderService.SubmitOrder(invalidOrder);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsNull_WithNull()
        {
            // Act
            var result = _processOrderService.SubmitOrder(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOrder_ReturnsExpectedOrder_WithExistingId()
        {
            //Arrange
            string id = "1";
            string[] items = {"item1", "item2"};
            IOrder expectOrder = new Order(

                name: "order1",
                id: "1",
                items: items
            );
            // Act
            var result = _processOrderService.GetOrder(id);

            // Assert
            //This Assert is ok because all properties are serialized
            Assert.AreEqual(JsonSerializer.Serialize(expectOrder), JsonSerializer.Serialize(result));
        }
        [TestMethod]
        public void GetOrder_ReturnsNull_WithNonExistingId()
        {
            //Arrange
            string id = "unknown id";

            // Act
            var result = _processOrderService.GetOrder(id);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "id must not be null")]
        public void GetOrder_Throws_WithNullId()
        {
            
            // Act
            var result = _processOrderService.GetOrder(null);

           
        }

    }
}