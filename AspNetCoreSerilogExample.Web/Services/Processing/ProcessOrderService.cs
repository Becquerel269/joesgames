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
        private readonly IFileProcessService _fileProcessService;

        public ProcessOrderService(IValidateOrderService validateOrderService, IOrderData orderData, IFileProcessService fileProcessService)
        {
            _validateOrderService = validateOrderService;
            _orderData = orderData;
            _fileProcessService = fileProcessService;
        }

        public IOrder GetOrder(string id)
        {

            if (id == null)
            {
                throw new ArgumentNullException("id must not be null");
            }
            if (GetFilepath(out var filepath)) return null;
            return _orderData.GetOrder(id, filepath);
        }

        private bool GetFilepath(out string filepath)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            filepath = $"{baseDirectory}/data/test.json";

            var fileokay = _fileProcessService.EnsureFileExists(filepath);
            if (!fileokay)
            {
                return true;
            }

            return false;
        }

        public IOrder SubmitOrder(Order order)
        {
           
            if (_validateOrderService.IsOrderValid(order) == false)
            {
                return null;
            }
            if (GetFilepath(out var filepath)) return null;

            return _orderData.SubmitOrder(order, filepath);
        }

        public List<Order> GetOrders()
        {
            if (GetFilepath(out var filepath)) return null;
            return _orderData.GetOrders(filepath);
        }
    }
}