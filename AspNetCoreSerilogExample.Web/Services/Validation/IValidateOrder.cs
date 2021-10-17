namespace AspNetCoreSerilogExample.Web.Services.Validation
{
    public interface IValidateOrder
    {
       bool IsOrderValid(string order);
    }
}