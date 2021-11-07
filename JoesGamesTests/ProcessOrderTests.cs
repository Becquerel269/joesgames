using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Moq;

namespace JoesGamesTests.ProcessOrderTests
{
    [TestClass]
    public class ProcessOrderTests
    {
        private Order validOrder;
        private Order invalidOrder;
        private IProcessOrderService _processOrderService;
        private IValidateOrderService _validateOrderService;
        private readonly Mock<IOrderData> _mockIOrderData = new Mock<IOrderData>();

        [TestInitialize]
        public void TestInitialize()
        {
            _validateOrderService = new ValidateOrderService();


            _processOrderService = new ProcessOrderService(_validateOrderService, _mockIOrderData.Object);
            string[] items = {"item1", "item2"};
            validOrder = new Order()
            {
                Name = "validname",
                Id = "1",
                Items = items
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
            _mockIOrderData.Setup(p => p.SubmitOrder(validOrder)).Returns(validOrder);


            // Act
            var result = _processOrderService.SubmitOrder(validOrder);

            // Assert
            Assert.AreEqual(validOrder.Id, result.Id);
            Assert.AreEqual(validOrder.Name, result.Name);
            Assert.AreEqual(validOrder.Items[0], result.Items[0]);
            Assert.AreEqual(validOrder.Items[1], result.Items[1]);
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


            string[] items = {"item1", "item2"};
            IOrder expectOrder = new Order(

                name: "order1",
                id: "1",
                items: items
            );
            _mockIOrderData.Setup(p => p.GetOrder(expectOrder.Id)).Returns(expectOrder);
            // Act
            var result = _processOrderService.GetOrder(expectOrder.Id);

            // Assert
            Assert.AreEqual(expectOrder.Id, result.Id);
            Assert.AreEqual(expectOrder.Name, result.Name);
            Assert.AreEqual(expectOrder.Items[0], result.Items[0]);
            Assert.AreEqual(expectOrder.Items[1], result.Items[1]);
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

        [TestMethod]
        public void GetOrders_ReturnsEmptyList_WithNoOrders()
        {
            //Arrange
            _mockIOrderData.Setup(p => p.GetOrders()).Returns(new List<Order>());

            // Act
            var result = _processOrderService.GetOrders();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetOrders_ReturnsListOfOrder_WithSomeOrders()
        {
            //Arrange
            List<Order> expectedOrders = new List<Order>();
            string[] firstOrderItems = {"item1", "item2"};
            var orderOne = new Order()
            {
                Name = "order1",
                Id = "1",
                Items = firstOrderItems
            };
            string[] secondOrderItems = {"item3", "item4"};
            var orderTwo = new Order()
            {
                Name = "order2",
                Id = "2",
                Items = secondOrderItems
            };
            expectedOrders.Add(orderOne);
            expectedOrders.Add(orderTwo);

            _mockIOrderData.Setup(p => p.GetOrders()).Returns(expectedOrders);


            // Act
            var result = _processOrderService.GetOrders();

            // Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(orderOne.Name, result.First().Name);
            Assert.AreEqual(orderTwo.Name, result.Last().Name);
        }

    }
}