using System.Collections.Generic;

#nullable enable
namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrder
    {
        string? Id { get; set; }
        IList<string> Items { get; set; }
        string Name { get; set; }
    }
}