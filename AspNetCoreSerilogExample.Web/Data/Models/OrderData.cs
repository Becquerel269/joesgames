using AspNetCoreSerilogExample.Web.Services.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderData : IOrderData
    {
        private readonly IFileProcessService _fileProcessService;

        public OrderData(IFileProcessService fileProcessService)
        {
            _fileProcessService = fileProcessService;
        }


        public IOrder GetOrder(string id)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(myJsonString);

            return orders?.FirstOrDefault(p => p.Id == id);
        }

        public IOrder SubmitOrder(Order order)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);
            if (myJsonString == "")
            {
                myJsonString = "[]";
            }

            var orders = JsonSerializer.Deserialize<List<Order>>(myJsonString);

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
                orders?.Add(order);
            }

            string serializedOrders = JsonSerializer.Serialize(orders);
            File.WriteAllText(filepath, serializedOrders);

            return order;

        }

        public List<Order> GetOrders()
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);

            var orders = JsonSerializer.Deserialize<List<Order>>(myJsonString);
            return orders;
        }

        private static string GetNewId(IOrder order, List<Order> orders)
        {
            if (orders.Count == 0)
            {
                order.Id = "1";
            }
            else
            {
                var currentMax = orders.Max(p => int.Parse(p.Id));
                order.Id = (currentMax + 1).ToString();
            }

            return order.Id;
        }

        private bool GetFilepath(out string filepath)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            filepath = $"{baseDirectory}/data/test.json";

            var fileokay = _fileProcessService.EnsureFileExists(filepath);
            if (!fileokay)
            {
                return true;
            }

            return false;
        }
    }
}