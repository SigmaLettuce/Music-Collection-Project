using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.IdentityModel.Tokens;

namespace CDOrganiserProjectApp
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=HomeMusicDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            view = new ConsoleView();
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
                case "view":
                    List<Bands> bands = storageManager.GetAllBands();
                    view.DisplayBands(bands);

                break;

                case "up":
                    UpdateBandName();


                break;

                case "ins":
                    // InsertNewBand();

                break;

                case "del":
                    // DeleteBandByName();

                break;

                default:
                    Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? :)");

                break;

            }
        }

        private static void UpdateBandName()
        {
            view.DisplayMessage("Enter the identification number... ");
            int bandId = view.GetIntInput();
            view.DisplayMessage("Rename the record... ");
            string bandName = view.GetInput();
            int rowsAffected = storageManager.UpdateBandName(bandId, bandName);
            view.DisplayMessage($"Updated {rowsAffected} records.");

        }

    }
        
    
}
