using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;

namespace CDOrganiserProjectApp
{
    public class Program
    {
        private static StorageManager storageManager;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=HomeMusicDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);

            List<Bands> bands = storageManager.GetAllBands();
            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.bandId}, {band.bandName}");
            }
        }
    }
}
