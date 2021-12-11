using Dapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderDataDB : IOrderData
    {
        //private const string connectionString = "Data Source=RYZEN-MEGA-BOX;Initial Catalog=JoesGames;Integrated Security=true";
        private readonly IConfiguration _configuration;

        private string _connectionString;
        private readonly ILogger _logger;

        public OrderDataDB(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("JoesGamesDB");
            _logger = logger;
        }

        public async Task<IOrderDTO> GetOrder(string orderId)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            con.Open();
            var parameters = new { OrderId = orderId };
            const string orderQuery = @"SELECT * FROM [dbo].[orders] WHERE ID = @OrderId";
            var order = (await con.QueryAsync<Order>(orderQuery, parameters)).FirstOrDefault();

            const string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE OrderID = @OrderId";
            var orderItems = await con.QueryAsync<OrderItem>(orderItemQuery, parameters);

            var orderDto = new OrderDTO
            {
                Name = order.Name,
                Id = order.Id,
                Items = orderItems.ToList()
            };
            return orderDto;
        }

        public async Task<IOrderDTO> SubmitOrder(OrderDTO orderDto)
        {
            orderDto.Id ??= Guid.NewGuid().ToString();
            using IDbConnection con = new SqlConnection(_connectionString);

            con.Open();
            var orderQueryParameters = new { Id = orderDto.Id, Name = orderDto.Name };
            const string orderQuery = @"INSERT INTO [dbo].[orders] (ID, Name) VALUES(@Id, @Name)";

            await con.QueryAsync<Order>(orderQuery, orderQueryParameters);
            var addedOrder = await GetOrder(orderDto.Id);
            var itemResults = new List<OrderItem>();
            //throw if table is deleted
            const string orderItemQuery = @"INSERT INTO [dbo].[OrderItems] (ID, OrderID, Name) VALUES(@Id, @OrderId, @Name)";
            foreach (var orderItem in orderDto.Items)
            {
                orderItem.Id ??= Guid.NewGuid().ToString();
                var orderItemQueryParameters = new { Id = orderItem.Id, OrderId = orderDto.Id, Name = orderItem.Name };
                (await con.QueryAsync<OrderItem>(orderItemQuery, orderItemQueryParameters)).FirstOrDefault();
                var addedOrderItem = await GetOrderItem(orderItem.Id);
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

        public async Task<int> UpdateOrder(OrderDTO orderDto)
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            var parameters = new { Name = orderDto.Name, ID = orderDto.Id };
            const string query = "UPDATE [dbo].[Orders] SET Name = @Name WHERE ID = @ID";
            return await con.ExecuteAsync(query, parameters);
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            const string query = @"SELECT * FROM dbo.orders ";
            var orders = (await con.QueryAsync<OrderDTO>(query)).ToList();
            return orders;
        }

        private async Task<OrderItem> GetOrderItem(string orderItemId)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            con.Open();
            var parameters = new { OrderItemId = orderItemId };
            const string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE ID = @OrderItemId";
            return (await con.QueryAsync<OrderItem>(orderItemQuery, parameters)).FirstOrDefault();
        }

        public async Task<int> DeleteOrder(string orderItemId)
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            var parameters = new { OrderItemId = orderItemId };
            const string query = "DELETE FROM [dbo].[OrderItems] WHERE ID = @OrderItemId";
            return await con.ExecuteAsync(query, parameters);
        }

        
        //read/watch into octopus deploy
        //in business layer for update, add a check for if order exists before calling submit in data layer
        //auto-mapping
        //research extension methods & create extension method for string to check if string has only white space or is null called 'isnullorblank' .isnullorblank()
    }
}