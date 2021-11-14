using System.Collections.Generic;
using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrderService
    {
        IOrderDTO SubmitOrder(OrderDTO orderdto);

        IOrderDTO GetOrder(string id);

        List<OrderDTO> GetOrders();
    }
}