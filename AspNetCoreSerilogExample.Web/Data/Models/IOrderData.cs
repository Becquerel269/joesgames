using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        IOrderDTO GetOrder(string orderId);

        IOrderDTO SubmitOrder(OrderDTO orderDto);

        List<OrderDTO> GetOrders();

        bool EnsureFileExists(string filepath);
    }
}