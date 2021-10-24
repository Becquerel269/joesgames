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
        private IProcessOrder processOrder;
        private IValidateOrder validateOrder;
        private IOrderData orderData;

        [TestInitialize]
        public void TestInitialize()
        {
            validateOrder = new ValidateOrder();
            orderData = new OrderData();
            processOrder = new ProcessOrder(validateOrder, orderData);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsTrue_WithValidOrder()
        {
            // Arrange
            string validorder = "Valid order";

            // Act
            bool result = processOrder.ProcessOrder(validorder);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsFalse_WithNotValidOrder()
        {
            // Arrange
            string invalidorder = "invalid order";

            // Act
            bool result = processOrder.ProcessOrder(invalidorder);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsFalse_WithNull()
        {
            // Act
            bool result = processOrder.ProcessOrder(null);

            // Assert
            Assert.IsFalse(result);
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
            var result = processOrder.GetOrder(id);

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
            var result = processOrder.GetOrder(id);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "id must not be null")]
        public void GetOrder_Throws_WithNullId()
        {
            
            // Act
            var result = processOrder.GetOrder(null);

           
        }
    }
}