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
            // Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=HomeMusicDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            view = new ConsoleView();
            string choice = view.DisplayMenu();
            bool invalid = false;

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

            do
            {
                switch (choice)
                {
                    case "view":
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);
                        invalid = false;

                    break;

                    case "up":
                        UpdateBandName();
                        invalid = false;

                    break;

                    case "ins":
                        // InsertNewBand();
                        invalid = false;

                    break;

                    case "del":
                        // DeleteBandByName();
                        invalid = false;

                    break;

                    default:
                        Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? :)");
                        Thread.Sleep(1000);

                        Console.Clear();

                        view.DisplayMenu();

                        invalid = true;

                    break;
                }
            } while (invalid);
            
        }

        private static void UpdateBandName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int bandId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string bandName = view.GetInput();

            int rowsAffected = storageManager.UpdateBandName(bandId, bandName);
            view.DisplayMessage($"Updated {rowsAffected} records.");

        }
        


    }
        
    
}
