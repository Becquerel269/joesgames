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
        string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=JoesGames;Integrated Security=true";
        public IOrder GetOrder(string id)
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"SELECT * FROM [dbo].[orders] WHERE ID = @id ";
                con.Execute(query, new {ID = id});


                var order = con.Query<Order>(query);
                return order.FirstOrDefault(p => p.Id == id);
            }
        }

        public IOrder SubmitOrder(Order order)
        {
            if (order.Id == null)
            {
                order.Id = Guid.NewGuid().ToString();
            }
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"INSERT INTO dbo.orders (ID, Name, Items) VALUES(@Id, @Name, @Items)";

                int rowsAffected = con.Execute(query, order);


                return order;
            }
        }

        public List<Order> GetOrders()
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"SELECT * FROM dbo.orders ";
                //using (SqlCommand command = new SqlCommand(query, con))
                //using (SqlDataReader reader = command.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {

                //        Console.WriteLine("reading");

                //    }
                //}
                var orders = con.Query<Order>(query).ToList();
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
    }
}
