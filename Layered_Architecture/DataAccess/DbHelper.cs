using Microsoft.Data.SqlClient;

namespace Layered_Architecture.DataAccess
{
    public static class DbHelper
    {
        public static string ConnectionString =
            @"Server=.\MSSQLSERVER02;Database=LayeredArchitectureDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}