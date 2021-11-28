using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class ProcessOrderService : IProcessOrderService
    {
        private readonly IValidateOrderService _validateOrderService;
        private readonly IOrderData _orderData;
        private readonly ILogger _logger;
  

        public ProcessOrderService(IValidateOrderService validateOrderService, IOrderData orderData, ILogger logger)
        {
            _validateOrderService = validateOrderService;
            _orderData = orderData;
            _logger = logger;
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

        public async Task<int> UpdateOrder(OrderDTO orderdto)
        {
            if (_validateOrderService.IsOrderValid(orderdto) == false)
            {
                return 400;
            }
            try
            {
                await _orderData.UpdateOrder(orderdto);
            }
            catch (Exception e)
            {

                _logger.Error("unable to update order {Orderdto}",orderdto);
                return 500;
            }
            return 200;
        }


        public async Task<List<OrderDTO>> GetOrders()
        {
            return await _orderData.GetOrders();
        }

        public async Task<IOrderDTO> DeleteOrder(string id)
        {
            return await _orderData.DeleteOrder(id);
        }
    }
}