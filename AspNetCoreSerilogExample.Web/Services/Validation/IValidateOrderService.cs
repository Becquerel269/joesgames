using AspNetCoreSerilogExample.Web.Data.Models;

namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public interface IValidateOrderService
    {
        bool IsOrderValid(IOrderDTO orderdto);
    }
}