
using System.Data.SqlClient;

namespace VirtualArtGallery.util
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=LAPTOP-TCFN1CUA;Initial Catalog=virtualartgallery;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}

