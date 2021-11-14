using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderItem : IOrderItem
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string OrderId { get; set; }
    }
}
