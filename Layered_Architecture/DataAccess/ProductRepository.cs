using Microsoft.Data.SqlClient;
using Layered_Architecture.Models;

namespace Layered_Architecture.DataAccess
{
    public class ProductRepository
    {
        public void Add(Product product)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "INSERT INTO Products(ProductName, Price) VALUES(@ProductName, @Price)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "SELECT ProductId, ProductName, Price FROM Products";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString() ?? "",
                    Price = Convert.ToDecimal(reader["Price"])
                });
            }

            return products;
        }

        public Product? GetById(int productId)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "SELECT ProductId, ProductName, Price FROM Products WHERE ProductId = @ProductId";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString() ?? "",
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }

            return null;
        }
    }
}