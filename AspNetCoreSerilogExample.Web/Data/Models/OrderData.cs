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

            Console.WriteLine(orders);
            return orders?.FirstOrDefault(p => p.Id == id);
        }

        public IOrder SubmitOrder(IOrder order)
        {
            //update validateorder tests - needs dummy order
            //create & merge PR

            
            //get existing orders from file
            //is this order in the file, if so replace
            //if not append to the array
            //replace the file with the updated array

            var myJsonString = File.ReadAllText(Filepath);

            Order[]? orders = JsonSerializer.Deserialize<Order[]>(myJsonString);

            Console.WriteLine(orders);

            if (orders == null)
            {
                orders = (Order[])Array.Empty<IOrder>();
            }

            var currentMax = orders.Max(p => Int32.Parse(p.Id));

            IOrder matchingOrder = null;
            if (order.Id == null)
            {
                order.Id = (currentMax + 1).ToString();
            }
            else
            {
                matchingOrder = orders.FirstOrDefault(p => p.Id == order.Id);
            }


            if (matchingOrder != null)
            {
                //update order
                matchingOrder = order;
            }
            else 
            {
                orders.Append(order);
            }

            
            File.WriteAllText(Filepath, myJsonString);

            //rename all service classes to have 'service' at the end


            return order;

        }
    }
}