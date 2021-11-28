using AspNetCoreSerilogExample.Web.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrderService
    {
        Task<IOrderDTO> SubmitOrder(OrderDTO orderdto);

        Task<IOrderDTO> GetOrder(string id);

        Task<List<OrderDTO>> GetOrders();

        Task<IOrderDTO> DeleteOrder(string id);
    }
}