using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrder
    {
        IOrder SubmitOrder(Order order);

        IOrder GetOrder(string id);
    }
}