using System;
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
            if (id == null)
            {
                throw new ArgumentNullException("id must not be null");
            } 
            return _orderData.GetOrder(id);
        }

        public IOrder SubmitOrder(IOrder order)
        {
            if (_validateOrder.IsOrderValid(order) == false)
            {
                return null;
            }

            return _orderData.SubmitOrder(order);
        }
    }
}