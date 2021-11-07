using System;
using System.Collections.Generic;
using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;

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

        public IOrder GetOrder(string id)
        {

            if (id == null)
            {
                throw new ArgumentNullException("id must not be null");
            }
            
            return _orderData.GetOrder(id);
        }

        

        public IOrder SubmitOrder(Order order)
        {
           
            if (_validateOrderService.IsOrderValid(order) == false)
            {
                return null;
            }
            

            return _orderData.SubmitOrder(order);
        }

        public List<Order> GetOrders()
        {
            
            return _orderData.GetOrders();
        }
    }
}