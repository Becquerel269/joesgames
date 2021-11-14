using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderDataDB : IOrderData
    {
        private const string connectionString = "Data Source=RYZEN-MEGA-BOX;Initial Catalog=JoesGames;Integrated Security=true";

        public IOrderDTO GetOrder(string orderId)
        {
            using IDbConnection con = new SqlConnection(connectionString);
            con.Open();
            var parameters = new { OrderId = orderId };
            const string orderQuery = @"SELECT * FROM [dbo].[orders] WHERE ID = @OrderId";
            var order = con.Query<Order>(orderQuery, parameters).FirstOrDefault();

            const string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE OrderID = @OrderId";
            var orderItems = con.Query<OrderItem>(orderItemQuery, parameters);

            var orderDto = new OrderDTO
            {
                Name = order.Name,
                Id = order.Id,
                Items = orderItems.ToList()
            };
            return orderDto;
        }

        public IOrderDTO SubmitOrder(OrderDTO orderDto)
        {
            orderDto.Id ??= Guid.NewGuid().ToString();
            using IDbConnection con = new SqlConnection(connectionString);

            con.Open();
            var orderQueryParameters = new { Id = orderDto.Id, Name = orderDto.Name };
            const string orderQuery = @"INSERT INTO [dbo].[orders] (ID, Name) VALUES(@Id, @Name)";

            con.Query<Order>(orderQuery, orderQueryParameters);
            var addedOrder = GetOrder(orderDto.Id);
            var itemResults = new List<OrderItem>();
            //throw if table is deleted
            const string orderItemQuery = @"INSERT INTO [dbo].[OrderItems] (ID, OrderID, Name) VALUES(@Id, @OrderId, @Name)";
            foreach (var orderItem in orderDto.Items)
            {
                orderItem.Id ??= Guid.NewGuid().ToString();
                var orderItemQueryParameters = new { Id = orderItem.Id, OrderId = orderDto.Id, Name = orderItem.Name };
                con.Query<OrderItem>(orderItemQuery, orderItemQueryParameters).FirstOrDefault();
                var addedOrderItem = GetOrderItem(orderItem.Id);
                itemResults.Add(addedOrderItem);
            }

            var addedOrderDto = new OrderDTO
            {
                Id = addedOrder.Id,
                Name = addedOrder.Name,
                Items = itemResults
            };

            return addedOrderDto;
        }

        public List<OrderDTO> GetOrders()
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            const string query = @"SELECT * FROM dbo.orders ";
            var orders = con.Query<OrderDTO>(query).ToList();
            return orders;
            //research extension methods & create extension method for string to check if string has only white space or is null called 'isnullorblank' .isnullorblank()
        }


        private static OrderItem GetOrderItem(string orderItemId)
        {
            using IDbConnection con = new SqlConnection(connectionString);
            con.Open();
            var parameters = new { OrderItemId = orderItemId };
            const string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE ID = @OrderItemId";
            return con.Query<OrderItem>(orderItemQuery, parameters).FirstOrDefault();
        }

        //delete method for order and orderitems
        //in business layer for update, add a check for if order exists before calling submit in data layer
        //auto-mapping
    }
}