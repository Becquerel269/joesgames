using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        IOrder GetOrder(string id);

        IOrder SubmitOrder(Order order);

        List<Order> GetOrders();

        bool EnsureFileExists(string filepath);
    }
}