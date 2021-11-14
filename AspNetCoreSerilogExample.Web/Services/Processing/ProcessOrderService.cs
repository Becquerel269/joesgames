using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;
using System;
using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class ProcessOrderService : IProcessOrderService
    {
        private readonly IValidateOrderService _validateOrderService;
        private readonly IOrderData _orderData;

        public ProcessOrderService(IValidateOrderService validateOrderService, IOrderData orderData)
        {
            _validateOrderService = validateOrderService;
            _orderData = orderData;
        }

        public IOrderDTO GetOrder(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id must not be null");
            }

            return _orderData.GetOrder(id);
        }

        public IOrderDTO SubmitOrder(OrderDTO orderdto)
        {
            if (_validateOrderService.IsOrderValid(orderdto) == false)
            {
                return null;
            }

            return _orderData.SubmitOrder(orderdto);
        }

        public List<OrderDTO> GetOrders()
        {
            return _orderData.GetOrders();
        }
    }
}