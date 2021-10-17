using AspNetCoreSerilogExample.Web.Services.Validation;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class ProcessOrder : IProcessOrder
    {
        private readonly IValidateOrder _validateOrder;
        public ProcessOrder(IValidateOrder validateOrder)
        {
            _validateOrder = validateOrder;
        }
        bool IProcessOrder.ProcessOrder(string order)
        {
            return _validateOrder.IsOrderValid(order);
        }
    }
}