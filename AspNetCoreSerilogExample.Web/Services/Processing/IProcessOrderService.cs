using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrderService
    {
        IOrder SubmitOrder(Order order);

        IOrder GetOrder(string id);
    }
}