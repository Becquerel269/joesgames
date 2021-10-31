using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrder
    {
        IOrder SubmitOrder(IOrder order);

        IOrder GetOrder(string id);
    }
}