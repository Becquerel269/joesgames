namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public class ValidateOrder : IValidateOrder
    {
        public bool IsOrderValid(string order)
        {
            if (order == null)
            {
                return false;
            }
            if (order.ToLower() == "valid order")
            {
                return true;
            }
            return false;
        }
    }
}