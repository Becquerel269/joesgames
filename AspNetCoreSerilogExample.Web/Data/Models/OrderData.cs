using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderData : IOrderData
    {
        public async Task<IOrderDTO> GetOrder(string orderId)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = await File.ReadAllTextAsync(filepath);

            List<OrderDTO> orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);

            return orders?.FirstOrDefault(p => p.Id == orderId);
        }

        public async Task<IOrderDTO> SubmitOrder(OrderDTO orderDto)
        {
            if (GetFilepath(out var filepath)) return null;
            var myJsonString = await File.ReadAllTextAsync(filepath);
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
            await File.WriteAllTextAsync(filepath, serializedOrders);

            return orderDto;
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            if (GetFilepath(out var filepath)) return null; //change out param
            var myJsonString = await File.ReadAllTextAsync(filepath);

            var orders = JsonSerializer.Deserialize<List<OrderDTO>>(myJsonString);
            return orders;
        }

        private static string GetNewId(IOrderDTO orderDto, List<OrderDTO> orders) //IReadOnlyCollection
        {
            if (orders.Count == 0)
            {
                orderDto.Id = "1";
            }
            else
            {
                var currentMax = orders.Max(p => int.Parse(p.Id));
                orderDto.Id = (currentMax + 1).ToString();
            }

            return orderDto.Id;
        }

        private static bool GetFilepath(out string filepath)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            filepath = $"{baseDirectory}/data/test.json";

            var fileOkay = EnsureFileExists(filepath);
            if (!fileOkay)
            {
                return true;
            }

            return false;
        }
        private static bool EnsureFileExists(string filepath)
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