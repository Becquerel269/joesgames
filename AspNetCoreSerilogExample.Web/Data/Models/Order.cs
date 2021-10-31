using System;
using System.Text.Json.Serialization;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class Order : IOrder
    {
        

        [JsonPropertyName(name: "name")]
        public string Name { get; set; }

        [JsonPropertyName(name: "id")]
        public string Id { get; set; }

        [JsonPropertyName(name: "items")]
        public string[] Items { get; set; }

        public Order()
        {
        }

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