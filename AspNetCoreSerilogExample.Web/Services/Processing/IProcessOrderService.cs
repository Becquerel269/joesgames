using AspNetCoreSerilogExample.Web.Data.Models;
using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrderService
    {
        IOrderDTO SubmitOrder(OrderDTO orderdto);

        IOrderDTO GetOrder(string id);

        List<OrderDTO> GetOrders();
    }
}