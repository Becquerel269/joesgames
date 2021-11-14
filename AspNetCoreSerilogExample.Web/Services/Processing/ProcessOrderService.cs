using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IOrderDTO> GetOrder(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id must not be null");
            }

            return await _orderData.GetOrder(id);
        }

        public async Task<IOrderDTO> SubmitOrder(OrderDTO orderdto)
        {
            if (_validateOrderService.IsOrderValid(orderdto) == false)
            {
                return null;
            }

            return await _orderData.SubmitOrder(orderdto);
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            return await _orderData.GetOrders();
        }
    }
}