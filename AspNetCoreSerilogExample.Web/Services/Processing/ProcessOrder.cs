using AspNetCoreSerilogExample.Web.Data.Models;
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

        public Order GetOrder(string id)
        {
            string[] items = { "item1", "item2" };
            Order order = new Order("fred", "fredid", items);
            return order;
        }

        bool IProcessOrder.ProcessOrder(string order)
        {
            return _validateOrder.IsOrderValid(order);
        }
    }
}