using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace AspNetCoreSerilogExample.Web.Data.Models
{
    public class OrderDataDB : IOrderData
    {
        string connectionString = "Data Source=RYZEN-MEGA-BOX;Initial Catalog=JoesGames;Integrated Security=true";
        public IOrderDTO GetOrder(string orderId)
        {


            using (IDbConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var parameters = new { OrderId = orderId };
                string orderQuery = @"SELECT * FROM [dbo].[orders] WHERE ID = @OrderId";
                var order = con.Query<Order>(orderQuery, parameters).FirstOrDefault();


  
                
                
                string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE OrderID = @OrderId";
                var orderItems = con.Query<OrderItem>(orderItemQuery, parameters);
                //var orderItems2 = con.Query<OrderItem>(orderItemQuery, parameters).FirstOrDefault();
                var orderDto = new OrderDTO
                {
                    Name = order.Name,
                    Id = order.Id,
                    Items = orderItems.ToList()
                };
                return orderDto;
                //return order.FirstOrDefault(p => p.Id == orderId);
            }
        }

        public IOrderDTO SubmitOrder(OrderDTO orderdto)
        {
            if (orderdto.Id == null)
            {
                orderdto.Id = Guid.NewGuid().ToString();
            }
            using (IDbConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                var orderQueryParameters = new {Id = orderdto.Id, Name = orderdto.Name};
                var orderQuery = @"INSERT INTO [dbo].[orders] (ID, Name) VALUES(@Id, @Name)";

                var result = con.Query<Order>(orderQuery, orderQueryParameters);

                var addedOrder = GetOrder(orderdto.Id);

                var itemResults = new List<OrderItem>();
                foreach (var orderItem in orderdto.Items)
                {
                    orderItem.Id ??= Guid.NewGuid().ToString();
                    var orderItemQueryParameters = new { Id = orderItem.Id,  OrderId = orderdto.Id, Name = orderItem.Name };
                    var orderItemQuery =
                        @"INSERT INTO [dbo].[OrderItems] (ID, OrderID, Name) VALUES(@Id, @OrderId, @Name)";
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
        }

        public List<OrderDTO> GetOrders()
        {


            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var query = @"SELECT * FROM dbo.orders ";
                
                var orders = con.Query<OrderDTO>(query).ToList();
                return orders;
            }


            //h/w
            //research DAPPER
            //research ASYNC
            //look at fixing the other methods in this class as above
            //research extension methods & create extension method for string to check if string has only white space or is null called 'isnullorblank' .isnullorblank()
        }

        public bool EnsureFileExists(string filepath)
        {
            throw new NotImplementedException();
        }

        private OrderItem GetOrderItem(string orderItemId)
        {
            using IDbConnection con = new SqlConnection(connectionString);
            con.Open();
            var parameters = new { OrderItemId = orderItemId };
            string orderItemQuery = "SELECT * FROM [dbo].[OrderItems] WHERE ID = @OrderItemId";
            return con.Query<OrderItem>(orderItemQuery, parameters).FirstOrDefault();
        }
    }
}
