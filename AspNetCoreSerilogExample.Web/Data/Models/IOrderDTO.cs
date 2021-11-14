using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public interface IOrderDTO
    {
        [JsonPropertyName(name: "Name")]
        public string Name { get; set; }

        [JsonPropertyName(name: "Id")]
        public string Id { get; set; }

        [JsonPropertyName(name: "Items")]
        public List<OrderItem> Items { get; set; }
    }
}
