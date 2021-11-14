using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderItem
    {
        string? Id { get; set; }
        string Name { get; set; }
        string OrderId { get; set; }
    }
}
