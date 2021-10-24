using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderData : IOrderData
    {
        public Order GetOrder(string id)
        {
            string[] items = { "item1", "item2" };
            Order order = new Order("fred", "fredid", items);
            return order;
        }
    }
}
