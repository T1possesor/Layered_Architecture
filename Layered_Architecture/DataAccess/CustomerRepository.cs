using Microsoft.Data.SqlClient;
using Layered_Architecture.Models;

namespace Layered_Architecture.DataAccess
{
    public class CustomerRepository
    {
        public void Add(Customer customer)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "INSERT INTO Customers(FullName, Phone) VALUES(@FullName, @Phone)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FullName", customer.FullName);
            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
            cmd.ExecuteNonQuery();
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "SELECT CustomerId, FullName, Phone FROM Customers";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    FullName = reader["FullName"].ToString() ?? "",
                    Phone = reader["Phone"].ToString() ?? ""
                });
            }

            return customers;
        }

        public bool Exists(int customerId)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql = "SELECT COUNT(1) FROM Customers WHERE CustomerId = @CustomerId";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}