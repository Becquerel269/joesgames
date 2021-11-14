#nullable enable

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrder
    {
        string? Id { get; set; }
        string Name { get; set; }
    }
}