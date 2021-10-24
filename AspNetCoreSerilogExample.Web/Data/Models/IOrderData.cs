using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderData
    {
        Order GetOrder(string id);
    }
}
