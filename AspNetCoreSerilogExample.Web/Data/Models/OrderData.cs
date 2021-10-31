using System;
using System.Collections.Generic;
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

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(myJsonString);

            Console.WriteLine(orders);
            return orders?.FirstOrDefault(p => p.Id == id);
        }

        public IOrder SubmitOrder(Order order)
        {
            //update validateorder tests - needs dummy order
            //create & merge PR

            
            //get existing orders from file
            //is this order in the file, if so replace
            //if not append to the array
            //replace the file with the updated array

            var myJsonString = File.ReadAllText(Filepath);
            if (myJsonString == "")
            {
                myJsonString = "[]";
            }

            var orders = JsonSerializer.Deserialize<List<Order>>(myJsonString);


            //if (orders == null)
            //{
            //    orders = (Order[])Array.Empty<IOrder>();
            //}

            
            new List<IOrder>();
            IOrder matchingOrder = null;
            if (order.Id == null)
            {
                order.Id = GetNewId(order, orders);
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
                orders.Add(order);
            }

            string serializedOrders = JsonSerializer.Serialize(orders);
            File.WriteAllText(Filepath, serializedOrders);

            //rename all service classes to have 'service' at the end


            return order;

        }

        private static string GetNewId(IOrder order, List<Order> orders)
        {
            if (orders.Count == 0)
            {
                order.Id = "1";
            }
            else
            {
                var currentMax = orders.Max(p => Int32.Parse(p.Id));
                order.Id = (currentMax + 1).ToString();
            }

            return order.Id;
        }
    }
}