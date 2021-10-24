using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class Order
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string[] Items { get; set; }

        public Order(string name, string id, string[] items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Name = name;
            Id = id;
            Items = items;

        }
        
    }
}
