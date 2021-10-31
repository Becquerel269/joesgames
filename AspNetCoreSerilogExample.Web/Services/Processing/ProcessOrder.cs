using System;
using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Validation;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class ProcessOrder : IProcessOrder
    {
        private readonly IValidateOrder _validateOrder;
        private readonly IOrderData _orderData;
        private readonly IFileProcessService _fileProcessService;

        public ProcessOrder(IValidateOrder validateOrder, IOrderData orderData, IFileProcessService fileProcessService)
        {
            _validateOrder = validateOrder;
            _orderData = orderData;
            _fileProcessService = fileProcessService;
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

            if (_validateOrder.IsOrderValid(order) == false)
            {
                return null;
            }
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            var fileokay = _fileProcessService.EnsureFileExists($"{baseDirectory}/data/test.json");
            if (!fileokay)
            {
                return null;
            }

            return _orderData.SubmitOrder(order);
        }
    }
}