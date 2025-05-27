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
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HomeMusicCollectionDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
             
            storageManager = new StorageManager(connectionString);
            view = new ConsoleView();
            string choice = view.DisplayMenu();
            bool invalid = true;

           
          /*  
            do
            {
                if (choice.Equals("view"))
                {
                    List<Bands> bands = storageManager.GetAllBands();
                    view.DisplayBands(bands);


                    invalid = false;
                }

                else if (choice.Equals("up"))
                {
                    UpdateBandName();

                    invalid = false;
                }

                else if (choice.Equals("ins"))
                {
                    InsertNewBand();

                    invalid = false;
                }

                else if (choice.Equals("del"))

                {
                    DeleteBandByName();
                    
                    invalid = false;
                }

                else
                {
                    Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again?");
                    view.DisplayMenu();

                    invalid = true;
                }
            } while (invalid);
            
        */                 
     
            do
            {
                switch (choice.ToLower())
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
                        InsertNewBand();
                        invalid = false;

                    break;

                    case "del":
                        DeleteBandByName();
                        invalid = false;

                    break;

                    default:
                        Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? ");
                        Thread.Sleep(1000);
                        Console.Clear();

                        choice = view.DisplayMenu();
                        invalid = true;

                    break;
                }
            } while (invalid);


            storageManager.CloseConnection();

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

        private static void InsertNewBand()
        {
            view.DisplayMessage("\nEnter the new band... ");
            string bandName = view.GetInput();
            int bandId = 0; // Fix this

            Bands newBand = new Bands(bandId, bandName);

            int generatedId = storageManager.InsertBand(newBand);
            view.DisplayMessage($"The new bands identification number is: {generatedId}");

        }

        private static void DeleteBandByName()
        {
            view.DisplayMessage("Enter the band you wish to erase from your records... ");
            string bandName = view.GetInput();

            int rowsAffected = storageManager.DeleteBandByName(bandName);
            view.DisplayMessage($"Deleted {rowsAffected} row.");
        }


    }
        
    
}
