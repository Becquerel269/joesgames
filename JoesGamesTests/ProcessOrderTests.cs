using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoesGamesTests.ProcessOrderTests
{
    [TestClass]
    public class ProcessOrderTests
    {
        private IProcessOrder processOrder;
        private IValidateOrder validateOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            validateOrder = new ValidateOrder();
            processOrder = new ProcessOrder(validateOrder);
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
    }
}
