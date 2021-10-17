using AspNetCoreMsLoggerExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoesGamesTests
{
    [TestClass]
    public class ValidateOrderTests
    {
        ValidateOrder validateOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            validateOrder = new ValidateOrder();
        }

        [TestMethod]
        public void IsOrderValidReturnsTrueWithValidOrder()
        {
            // Arrange
            string validorder = "Valid order";
            

            // Act
            bool result = validateOrder.IsOrderValid(validorder);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsOrderValidReturnsFalseWithNotValidOrder()
        {
            // Arrange
            string invalidorder = "invalid order";
            

            // Act
            bool result = validateOrder.IsOrderValid(invalidorder);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsOrderValidReturnsFalseWithNull()
        {
           
            // Act
            bool result = validateOrder.IsOrderValid(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}