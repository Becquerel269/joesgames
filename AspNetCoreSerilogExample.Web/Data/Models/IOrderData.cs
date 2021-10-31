namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        IOrder GetOrder(string id);

        IOrder SubmitOrder(IOrder order);
    }
}