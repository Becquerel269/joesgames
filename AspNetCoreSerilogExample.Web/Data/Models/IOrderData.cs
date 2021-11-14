using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        IOrderDTO GetOrder(string orderId);

        IOrderDTO SubmitOrder(OrderDTO orderdto);

        List<OrderDTO> GetOrders();

        bool EnsureFileExists(string filepath);
    }
}