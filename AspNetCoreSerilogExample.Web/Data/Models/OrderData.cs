using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderData : IOrderData
    {
        public IOrderDTO GetOrder(string orderId)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);

            List<OrderDTO> orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);

            return orders?.FirstOrDefault(p => p.Id == orderId);
        }

        public IOrderDTO SubmitOrder(OrderDTO orderDto)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);
            if (myJsonString == "")
            {
                myJsonString = "[]";
            }

            var orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);

            IOrderDTO matchingOrder = null;
            if (orderDto.Id == null)
            {
                orderDto.Id = GetNewId(orderDto, orders);
            }
            else
            {
                matchingOrder = orders.FirstOrDefault(p => p.Id == orderDto.Id);
            }

            if (matchingOrder != null)
            {
                //update order
                matchingOrder = orderDto;
            }
            else
            {
                orders?.Add(orderDto);
            }

            string serializedOrders = JsonSerializer.Serialize(orders);
            File.WriteAllText(filepath, serializedOrders);

            return orderDto;
        }

        public List<OrderDTO> GetOrders()
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);

            var orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);
            return orders;
        }

        private static string GetNewId(IOrderDTO orderdto, List<OrderDTO> orders)
        {
            if (orders.Count == 0)
            {
                orderdto.Id = "1";
            }
            else
            {
                var currentMax = orders.Max(p => int.Parse(p.Id));
                orderdto.Id = (currentMax + 1).ToString();
            }

            return orderdto.Id;
        }

        private bool GetFilepath(out string filepath)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            filepath = $"{baseDirectory}/data/test.json";

            var fileokay = EnsureFileExists(filepath);
            if (!fileokay)
            {
                return true;
            }

            return false;
        }
        private bool EnsureFileExists(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            try
            {
                File.Create(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"unable to create file with filepath {filepath}");
            }

            return false;
        }
    }
}