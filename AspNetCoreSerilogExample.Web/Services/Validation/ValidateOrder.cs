using AspNetCoreSerilogExample.Web.Data.Models;
using static System.String;

namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public class ValidateOrder : IValidateOrder
    {
        public bool IsOrderValid(IOrder order)
        {
            if (order == null)
            {
                return false;
            }
            if (IsNullOrWhiteSpace(order.Name))
            {
                return false;
            }
            return true;
        }
    }
}