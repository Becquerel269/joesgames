using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.DataLayer.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public Order(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
