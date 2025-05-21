using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;

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
            ConsoleView view = new ConsoleView();
            string choice = view.DisplayMenu();

            /*
                if (choice.Equals("1"))
                {
                    view.DisplayBands(storageManager.GetAllBands());


                }

                else if (choice.Equals("2"))
                {
                    view.DisplayArtists(storageManager.GetAllArtists());


                }

                else
                {
                    Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? :)");
                    view.DisplayMenu();
                }



            -- If statment variant.

            */


                switch (choice)
                {
                    case "1":
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);

                    break;

                    case "2":
                        UpdateBandName();


                    break;

                    case "3":
                        InsertNewBand();

                    break;

                    case "4":
                        DeleteBandByName();

                    break;

                    default:
                        Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? :)");

                    break;

                }


        }
    }
}
