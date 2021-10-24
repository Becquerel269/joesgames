using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderData : IOrderData
    {
        private const string Filepath = "Data/Models/orders.json";

        public IOrder GetOrder(string id)
        {
            var myJsonString = File.ReadAllText(Filepath);

            Order[]? orders = JsonSerializer.Deserialize<Order[]>(myJsonString);

            Console.Out.WriteLine(orders);
            return orders?.FirstOrDefault(p => p.Id == id);
        }
    }
}