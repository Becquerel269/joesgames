using System.Text.Json.Serialization;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class Order : IOrder
    {
        [JsonPropertyName(name: "Name")]
        public string Name { get; set; }

        [JsonPropertyName(name: "Id")]
        public string Id { get; set; }

        //[JsonPropertyName(name: "Items")]
        //public IList<string> Items { get; set; }

        public Order()
        {
        }

        public Order(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}