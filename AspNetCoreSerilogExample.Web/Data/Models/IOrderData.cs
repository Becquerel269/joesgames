using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        IOrder GetOrder(string id, string filepath);

        IOrder SubmitOrder(Order order, string filepath);

        List<Order> GetOrders(string filepath);
    }
}