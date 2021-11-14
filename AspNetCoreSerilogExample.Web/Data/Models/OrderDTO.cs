using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderDTO : IOrderDTO
    {
        [JsonPropertyName(name: "Name")]
        public string Name { get; set; }

        [JsonPropertyName(name: "Id")]
        public string Id { get; set; }

        [JsonPropertyName(name: "Items")]
        public List<OrderItem> Items { get; set; }

        public OrderDTO()
        {

        }
        public OrderDTO(string name, string id, List<OrderItem> items)
        {

            Name = name;
            Id = id;
            Items = items;
        }
    }
}
