using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public interface IValidateOrder
    {
        bool IsOrderValid(IOrder order);
    }
}