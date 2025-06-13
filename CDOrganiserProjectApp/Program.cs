using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Index;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
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

            MenuscreenOptions();

            storageManager.CloseConnection();

        }

        private static void MenuscreenOptions()
        {
            
            string adminInput = view.DisplayAdministratorMenu();
            view.DisplayMessage("");

            bool invalid = true;

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

                    case Prefix.view + " " + Suffix.bands:
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.up + " " + Suffix.bands:
                        UpdateBandName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.ins + " " + Suffix.bands:
                        InsertNewBand();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.del + " " + Suffix.bands:
                        DeleteBandByName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.view + " " + Suffix.artists:
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.up + " " + Suffix.artists:
                        UpdateArtistName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.ins + " " + Suffix.artists:
                        InsertNewArtist();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.del + " " + Suffix.artists:
                        DeleteArtistByName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.view + " " + Suffix.albums:
                        List<Albums> albums = storageManager.GetAllAlbums();
                        view.DisplayAlbums(albums);

                        invalid = false;

                        GoBack();

                    break;

                    default:
                        view.DisplayMessage("I'm sorry, this isn't a valid selection. Can you try again? ");
                        Thread.Sleep(1000);
                        Console.Clear();

                        adminInput = view.DisplayAdministratorMenu();
                        view.DisplayMessage("");
                        invalid = true;

                    break;

                }

            } while (invalid);

        }

        private static void GoBack() 
        {
            string input;
            bool carry;


            view.DisplayMessage("\nGo back? y/n\n");
            input = view.GetInput();


            do
                switch (input.ToLower())
                {
                    case "y":
                        Thread.Sleep(1000);
                        Console.Clear();
                        carry = false;

                        MenuscreenOptions();
                        view.DisplayMessage("");

                    break;

                    case "n":
                        GoBack();

                        carry = true;

                    break;

                    default:
                        view.DisplayMessage("\nI'm sorry, this isn't a valid selection. Can you try again ? ");
                        GoBack();

                        carry = true;

                    break;

                }

            while (carry);


        }

        private static void UpdateBandName()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string bandName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateBandByName(bandName, rename);
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
            view.DisplayMessage("\nEnter the name of the record... ");
            string artistName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateArtistByName(artistName, rename);
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
