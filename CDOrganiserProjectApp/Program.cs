using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

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
            string adminInput = view.DisplayAdministratorMenu();

            bool invalid = true;


            /*
             do
             {  

                 string[] prefix = 
                 { 
                     "view", 
                     "up", 
                     "ins", 
                     "del", 
                     "lost" 

                 };

                 string[] suffix = 
                 { 
                     "albums", 
                     "artists", 
                     "bands", 
                     "all", 
                     "rooms" 

                 };

                 if (choice.Equals(prefix[0] + " " + suffix[2]))
                 {
                     List<Bands> bands = storageManager.GetAllBands();
                     view.DisplayBands(bands);


                     invalid = false;
                 }

                 else if (choice.Equals(prefix[1] + " " + suffix[2]))
                 {
                     UpdateBandName();

                     invalid = false;
                 }

                 else if (choice.Equals(prefix[2] + " " + suffix[2]))
                 {
                     InsertNewBand();

                     invalid = false;
                 }

                 else if (choice.Equals(prefix[3] + " " + suffix[2]))

                 {
                     DeleteBandByName();

                     invalid = false;
                 }

                 else if (choice.Equals(prefix[0] + " " + suffix[1]))
                 {
                     List<Artists> artists = storageManager.GetAllArtists();
                     view.DisplayArtists(artists);


                     invalid = false;
                 }

                 else if (choice.Equals(prefix[1] + " " + suffix[1]))
                 {
                     UpdateArtistName();

                     invalid = false;
                 }

                 else if (choice.Equals(prefix[2] + " " + suffix[1]))
                 {
                     InsertNewArtist();

                     invalid = false;
                 }

                 else if (choice.Equals(prefix[3] + " " + suffix[1]))

                 {
                     DeleteArtistByName();

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

            /*
            
            
            do
            {
                switch (switch_on)
                {
                    default:
                }

            } while (invalid);

            This is for the login.

            do
            {
                switch (switch_on)
                {
                    default:
                }

            } while (invalid);

            This is for the register.

            */


            do
            {
                switch (adminInput.ToLower())
                {
                    case "view" + " " + "bands":
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);
             
                        invalid = false;

                    break;
                    
                    case "up" + " " + "bands":
                        UpdateBandName();

                        invalid = false;

                    break;

                    case "ins" + " " + "bands":
                        InsertNewBand();
                        invalid = false;

                    break;
                    
                    case "del" + " " + "bands":
                        DeleteBandByName();

                        invalid = false;

                    break;

                    case "view" + " " + "artists":
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        invalid = false;

                    break;

                    case "up" + " " + "artists":
                        UpdateArtistName();

                        invalid = false;

                    break;

                    case "ins" + " " + "artists":
                        InsertNewArtist();

                        invalid = false;

                    break;

                    case "del" + " " + "artists":
                        DeleteArtistByName();

                        invalid = false;

                    break;

                    case "view" + " " + "albums":
                        List<Albums> albums = storageManager.GetAllAlbums();
                        view.DisplayAlbums(albums);

                        invalid = false;

                    break;

                    default:
                        Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? ");
                        Thread.Sleep(1000);
                        Console.Clear();

                        adminInput = view.DisplayAdministratorMenu();

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
            int bandId = 0; 

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

        private static void UpdateArtistName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int artistId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string artistName = view.GetInput();

            int rowsAffected = storageManager.UpdateBandName(artistId, artistName);
            view.DisplayMessage($"Updated {rowsAffected} records.");

        }

        private static void InsertNewArtist()
        {
            view.DisplayMessage("\nEnter the new artist... ");
            string artistName = view.GetInput();
            int artistId = 0;

            Artists newArtist = new Artists(artistId, artistName);

            int generatedId = storageManager.InsertArtist(newArtist);
            view.DisplayMessage($"The new artists identification number is: {generatedId}");

        }

        private static void DeleteArtistByName()
        {
            view.DisplayMessage("Enter the artist you wish to erase from your records... ");
            string artistName = view.GetInput();

            int rowsAffected = storageManager.DeleteArtistByName(artistName);
            view.DisplayMessage($"Deleted {rowsAffected} row.");
        }


    }


}
