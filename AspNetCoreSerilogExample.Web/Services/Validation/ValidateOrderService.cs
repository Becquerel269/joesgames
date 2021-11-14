using AspNetCoreSerilogExample.Web.Data.Models;
using static System.String;

namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public class ValidateOrderService : IValidateOrderService
    {
        public bool IsOrderValid(IOrderDTO orderdto)
        {
            if (orderdto == null)
            {
                return false;
            }
            if (IsNullOrWhiteSpace(orderdto.Name))
            {
                return false;
            }
            return true;
        }
    }
}