using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrder
    {
        bool ProcessOrder(string order);

        IOrder GetOrder(string id);
    }
}