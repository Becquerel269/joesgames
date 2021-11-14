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


        public bool EnsureFileExists(string filepath)
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

        public IOrderDTO GetOrder(string orderId)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);

            List<OrderDTO> orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);

            return orders?.FirstOrDefault(p => p.Id == orderId);
        }

        public IOrderDTO SubmitOrder(OrderDTO orderdto)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = File.ReadAllText(filepath);
            if (myJsonString == "")
            {
                myJsonString = "[]";
            }

            var orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);

            IOrderDTO matchingOrder = null;
            if (orderdto.Id == null)
            {
                orderdto.Id = GetNewId(orderdto, orders);
            }
            else
            {
                matchingOrder = orders.FirstOrDefault(p => p.Id == orderdto.Id);
            }


            if (matchingOrder != null)
            {
                //update order
                matchingOrder = orderdto;
            }
            else
            {
                orders?.Add(orderdto);
            }

            string serializedOrders = JsonSerializer.Serialize(orders);
            File.WriteAllText(filepath, serializedOrders);

            return orderdto;

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
    }
}