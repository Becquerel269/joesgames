using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class ProcessOrder : IProcessOrder
    {
        private readonly IValidateOrder _validateOrder;
        private readonly IOrderData _orderData;

        public ProcessOrder(IValidateOrder validateOrder, IOrderData orderData)
        {
            _validateOrder = validateOrder;
            _orderData = orderData;
        }

        public IOrder GetOrder(string id)
        {
            return _orderData.GetOrder(id);
        }

        bool IProcessOrder.ProcessOrder(string order)
        {
            return _validateOrder.IsOrderValid(order);
        }
    }
}