using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        Task<IOrderDTO> GetOrder(string orderId);

        Task<IOrderDTO> SubmitOrder(OrderDTO orderDto);

        Task<List<OrderDTO>> GetOrders();

        Task<int> DeleteOrder(string id);

        Task<int> UpdateOrder(OrderDTO orderDto);
    }
}