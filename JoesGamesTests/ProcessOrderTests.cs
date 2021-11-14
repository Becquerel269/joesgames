using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JoesGamesTests.ProcessOrderTests
{
    [TestClass]
    public class ProcessOrderTests
    {
        private OrderDTO validOrderDto;
        private OrderDTO invalidOrderDto;
        private IProcessOrderService _processOrderService;
        private IValidateOrderService _validateOrderService;
        private readonly Mock<IOrderData> _mockIOrderData = new Mock<IOrderData>();

        [TestInitialize]
        public void TestInitialize()
        {
            _validateOrderService = new ValidateOrderService();

            _processOrderService = new ProcessOrderService(_validateOrderService, _mockIOrderData.Object);
            string[] items = { "item1", "item2" };
            validOrderDto = new OrderDTO()
            {
                Name = "validname",
                Id = "1",
            };
            invalidOrderDto = new OrderDTO()
            {
                Name = null,
                Id = null,
            };
        }

        [TestMethod]
        public void ProcessOrder_ReturnsTrue_WithValidOrder()
        {
            // Arrange
            _mockIOrderData.Setup(p => p.SubmitOrder(validOrderDto)).ReturnsAsync(validOrderDto);

            // Act
            var result = _processOrderService.SubmitOrder(validOrderDto).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(validOrderDto.Id, result.Id);
            Assert.AreEqual(validOrderDto.Name, result.Name);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsFalse_WhenOrderNameNotSupplied()
        {
            // Arrange

            // Act
            var result = _processOrderService.SubmitOrder(invalidOrderDto).GetAwaiter().GetResult();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProcessOrder_ReturnsNull_WithNull()
        {
            // Act
            var result = _processOrderService.SubmitOrder(null).GetAwaiter().GetResult();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOrder_ReturnsExpectedOrder_WithExistingId()
        {
            //Arrange

            IOrderDTO expectOrderdto = new OrderDTO(

                name: "order1",
                id: "1",
                items: new List<OrderItem>()

            );
            _mockIOrderData.Setup(p => p.GetOrder(expectOrderdto.Id)).ReturnsAsync(expectOrderdto);
            // Act
            var result = _processOrderService.GetOrder(expectOrderdto.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(expectOrderdto.Id, result.Id);
            Assert.AreEqual(expectOrderdto.Name, result.Name);
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
            var result = _processOrderService.GetOrder(null).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetOrders_ReturnsEmptyList_WithNoOrders()
        {
            //Arrange
            _mockIOrderData.Setup(p => p.GetOrders()).ReturnsAsync(new List<OrderDTO>());

            // Act
            var result = _processOrderService.GetOrders().GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetOrders_ReturnsListOfOrder_WithSomeOrders()
        {
            //Arrange
            List<OrderDTO> expectedOrders = new List<OrderDTO>();
            string[] firstOrderItems = { "item1", "item2" };
            var orderOne = new OrderDTO()
            {
                Name = "order1",
                Id = "1",
            };
            string[] secondOrderItems = { "item3", "item4" };
            var orderTwo = new OrderDTO()
            {
                Name = "order2",
                Id = "2",
            };
            expectedOrders.Add(orderOne);
            expectedOrders.Add(orderTwo);

            _mockIOrderData.Setup(p => p.GetOrders()).ReturnsAsync(expectedOrders);

            // Act
            var result = _processOrderService.GetOrders().GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(orderOne.Name, result.First().Name);
            Assert.AreEqual(orderTwo.Name, result.Last().Name);
        }
    }
}