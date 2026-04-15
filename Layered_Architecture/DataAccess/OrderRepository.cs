using Microsoft.Data.SqlClient;
using Layered_Architecture.Models;

namespace Layered_Architecture.DataAccess
{
    public class OrderRepository
    {
        public void Add(Order order)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = @"INSERT INTO Orders(CustomerId, ProductId, Quantity, OrderDate)
                           VALUES(@CustomerId, @ProductId, @Quantity, @OrderDate)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.ExecuteNonQuery();
        }

        public List<OrderDetail> GetAllDetails()
        {
            List<OrderDetail> orders = new List<OrderDetail>();

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = @"
                SELECT 
                    o.OrderId,
                    c.FullName AS CustomerName,
                    p.ProductName,
                    p.Price,
                    o.Quantity,
                    o.OrderDate,
                    (p.Price * o.Quantity) AS TotalAmount
                FROM Orders o
                INNER JOIN Customers c ON o.CustomerId = c.CustomerId
                INNER JOIN Products p ON o.ProductId = p.ProductId
                ORDER BY o.OrderId DESC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new OrderDetail
                {
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    CustomerName = reader["CustomerName"].ToString() ?? "",
                    ProductName = reader["ProductName"].ToString() ?? "",
                    Price = Convert.ToDecimal(reader["Price"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                });
            }

            return orders;
        }
    }
}