using AspNetCoreMsLoggerExample.Web.Services.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    [TestClass]
    public class ValidateOrderTests
    {
        [TestMethod]
        public void IsOrderValidReturnsTrueWithValidOrder()
        {
            // Arrange
            string validorder = "valid order";
            ValidateOrder validateOrder = new ValidateOrder();

            // Act
            bool result = validateOrder.IsOrderValid(validorder);

            // Assert
            Assert.IsTrue(result);
        }
    }
}