using Microsoft.Data.SqlClient;

namespace CDOrganiserProjectApp.Repositories
{
    // Manages connection to database, passes queries etc.
    public class StorageManager
    {
        private SqlConnection conn;

        public StorageManager(string connectionStr)
        {
            try
            {
                conn = new SqlConnection(connectionStr);
                conn.Open();
                Console.WriteLine("Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed.\n");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
