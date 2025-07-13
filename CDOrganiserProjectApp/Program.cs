using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace CDOrganiserProjectApp
{
    public class Program
    {

        /*                   *\  
         
               [PROGRAM]
         
        \*                   */

        private static StorageManager storageManager;
        private static ConsoleView view;

        static int accountId;
        static int roleId;
        static bool logStatus;

        const int wait = 1000; 

        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");

            view = new ConsoleView();

            StartMenuscreenOptions();
            //AdminMenuscreenOptions();

        }

        private static void StartMenuscreenOptions()
        {

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HomeMusicCollectionDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            storageManager = new StorageManager(connectionString);

            bool invalid = true; // A variable that evaluates the continuation of a process.


            string startInput = view.StartMenu(); // Calls the display, and returns a prompt for input.
            view.DisplayMessage("");
             
            do
            {
                switch (startInput.ToUpper())
                {
                    case "R":

                        Register(); // This calls the register. 

                        invalid = false; // Overwrites the boolean so that the Register can proceed.

                    break;

                    case "L":

                        Login(); // This calls the login.

                        invalid = false;

                    break;

                    case "H":

                        Help(); // This calls the support page.

                        invalid = false;

                    break;

                    default:
                        view.DisplayError(wait); // This calls the error prompt. It's parameterized simply to utilise the same threadsleep count as the one used in the main Program, as the ConsoleView already contains a threadsleep.

                        StartMenuscreenOptions();

                        invalid = true; // The condition is evaluated to match the conditions of the do ...while. The block of code stays alive.

                    break;
                }

            }  while (invalid);



            
        }

        private static void Register() // The Register method. Essentially an insert.
        {
            Thread.Sleep(wait);
            Console.Clear();

            CreateUser();
            Thread.Sleep(wait);
            Console.Clear();
            // GuestMenuscreenOptions();


        }

        private static void Login() // The Login method. When the username and password are provided, they cross-examine each other and if both are a match you either get logged in as an administrator, user or a true administrator. The accounts identification number is also fetched to tailor 'to query' specific data listings.
        {
            Thread.Sleep(wait);
            Console.Clear();

            view.DisplayMessage("\nEnter your username... ");
            view.DisplayMessage(" ");
            string user = view.GetInput();


            view.DisplayMessage("\nEnter your password... ");
            view.DisplayMessage(" ");
            string pw = view.GetInput();


            string fetchUsername = storageManager.FetchUsername(user, pw);
            string fetchPassword = storageManager.FetchPassword(user, pw);
            accountId = storageManager.FetchAccount(user, pw);
            roleId = storageManager.FetchRole(user, pw);



            if (user.Equals(fetchUsername) & pw.Equals(fetchPassword))
            {
                switch (roleId)
                {
                    case 1:
                        Thread.Sleep(wait);
                        Console.Clear();

                        GuestMenuscreenOptions();

                    break;

                    case 2:
                        Thread.Sleep(wait);
                        Console.Clear();

                        AdminMenuscreenOptions();

                    break;
                }
            }

            else
            {
                view.DisplayMessage("\nEither your username or password are incorrect. ");
                Thread.Sleep(wait);
                Console.Clear();

                Login();
            }

        }

        private static void Help()
        {
            bool invalid = true;

            string input = view.DisplayHelp(wait);
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (input.ToUpper())
                {
                    case "E":

                        Thread.Sleep(wait);
                        Console.Clear();

                        switch (logStatus)
                        {
                            case true:
                                switch (roleId)
                                {
                                    case 1:
                                        GuestMenuscreenOptions();

                                    break;

                                    case 2:
                                        AdminMenuscreenOptions();

                                    break;
                                  
                                }

                            break;

                            case false:
                            StartMenuscreenOptions();

                            break;

                        }

                    break;

                    default:
                        view.DisplayError(wait);

                        Help();

                    break;
                }
            } while (invalid);
            
        }

        private static void AdminMenuscreenOptions() // The Admin Menuscreen. 
        {           
            string select; // Stores a selection from a listing that branches into multiple.
            string cmd; // Stores the users written modification command.
            bool invalid = true;

            string input = view.DisplayAdminMenu(); // Calls the display.
            view.DisplayMessage(""); 

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (input.ToLower())
                {

                    case "bands":
                        
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("bands", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
                            {
                                case "up":
                                    UpdateBandName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewBand();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteBandById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "reports":

                                    invalid = false;

                                break;

                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                  
                    case "artists":
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("artists", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "up":
                                    UpdateArtistName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewArtist();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteArtistById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "reports":


                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                                        
                    case "albums":

                        Thread.Sleep(wait);

                        select = view.DisplayEditingOptions("albums", "album~variants");

                        Thread.Sleep(wait);
                        Console.Clear();

                        invalid = false;

                        do
                        {
                            switch (select)
                            {
                                case "artists":
                                    storageManager.GetAllArtistAlbums();
                                    
                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("artist-albums", "album~extras");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        {
                                            case "up":
                                                UpdateArtistAlbum();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "ins":
                                                InsertArtistAlbum();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "del":
                                                DeleteArtistAlbumById();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "lost":
                                                MarkArtistAsLost();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                AdminMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandAlbums();

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("band-albums", "album~extras");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        {
                                            case "up":
                                                UpdateBandAlbum();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "ins":
                                                InsertBandAlbum();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "del":
                                                DeleteBandAlbumById();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "lost":
                                                MarkBandAsLost();

                                                invalid = false;

                                                GoBack();
                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                AdminMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);
                    break;

                    case "reviews":

                        Thread.Sleep(wait);

                        select = view.DisplayEditingOptions("reviews", "album~variants");

                        Thread.Sleep(wait);
                        Console.Clear();

                        invalid = false;

                        do
                        {
                            switch (select)
                            {
                                case "artists":
                                    storageManager.GetAllArtistReviews(accountId);

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("artist-album-reviews", "review~extras");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        {
                                            case "up":
                                                UpdateArtistReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "ins":
                                                InsertArtistReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "del":
                                                DeleteArtistReviewById();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "favourite":
                                                FavouriteArtistReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                AdminMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandReviews(accountId);

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("band-album-reviews", "review~extras");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        {
                                            case "up":
                                                UpdateBandReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "ins":
                                                InsertBandReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "del":
                                                DeleteBandReviewById();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "favourite":
                                                FavouriteBandReview();

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                AdminMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);
                    break;

                    case "genres":
                        List<Genres> genres = storageManager.GetAllGenres();
                        view.DisplayGenres(genres);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("genres", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "up":
                                    UpdateArtistName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    UpdateArtistAlbum();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteArtistById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "formats":
                        List<Formats> formats = storageManager.GetAllFormats();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("formats", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "up":
                                    UpdateFormatName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewFormat();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteFormatById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "rooms":
                        storageManager.GetAllRooms();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("rooms", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "up":
                                    UpdateRoomName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewRoom();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteRoomById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "shelves":
                        storageManager.GetAllShelves();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("shelves", "default");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "up":
                                    UpdateShelfRoom();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewShelf();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteShelfById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "tiers":
                        List<Tiers> tiers = storageManager.GetAllTiers();
                        view.DisplayTiers(tiers);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("tiers", "none");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
                            {

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "accounts":

                        Thread.Sleep(wait);

                        select = view.DisplayEditingOptions("accounts", "account~variants");

                        Thread.Sleep(wait);
                        Console.Clear();

                        do
                        {
                            switch (select)
                            {
                                case "default":
                                    CreateUser();

                                    GoBack();

                                    invalid = false;

                                break;

                                case "admin":
                                    CreateAdmin();

                                    GoBack();

                                    invalid = false;

                                break;

                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    AdminMenuscreenOptions();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);

                    break;

                    case "help":

                        logStatus = true;
                        Thread.Sleep(wait);

                        Help(); // This calls the support page.

                        Thread.Sleep(wait);

                        invalid = false;

                    break;

                    case "l":
                        Thread.Sleep(wait);
                        Console.Clear();

                        // Resets the accounts credentials.

                        roleId = 0;
                        accountId = 0;
                        storageManager.CloseConnection(); // Closes the connection upon signing out.
                        StartMenuscreenOptions();

                        invalid = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        AdminMenuscreenOptions();

                        invalid = true;

                    break;

                }

            } while (invalid);

        }

        private static void GuestMenuscreenOptions() // The Guest Menuscreen. 
        {           
            string select; // Stores a selection from a listing that branches into multiple.
            string cmd; // Stores the users written modification command.
            bool invalid = true;

            string input = view.DisplayGuestMenu(); // Calls the display.
            view.DisplayMessage(""); 

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (input.ToLower())
                {

                    case "bands":
                        
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("bands", "none");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
                            {
                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GuestMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                  
                    case "artists":
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("artists", "none");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GuestMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                                        
                    case "albums":

                        Thread.Sleep(wait);

                        select = view.DisplayEditingOptions("albums", "album~variants");

                        Thread.Sleep(wait);
                        Console.Clear();

                        invalid = false;

                        do
                        {
                            switch (select)
                            {
                                case "artists":
                                    storageManager.GetAllArtistReviews(accountId);

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("artist-albums", "none");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        {
                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GuestMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandReviews(accountId);

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("band-albums", "none");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    invalid = false;

                                    do
                                    {

                                        switch (cmd)
                                        { 
                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GuestMenuscreenOptions();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GuestMenuscreenOptions();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);
                    break;

                    case "genres":
                        List<Genres> genres = storageManager.GetAllGenres();
                        view.DisplayGenres(genres);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("genres", "none");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
                            {
                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GuestMenuscreenOptions();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "accounts":

                        Thread.Sleep(wait);

                        select = view.DisplayEditingOptions("accounts", "df~account~variants");

                        Thread.Sleep(wait);
                        Console.Clear();

                        do
                        {
                            switch (select)
                            {
                                case "default":
                                    CreateUser();

                                    GoBack();

                                    invalid = false;

                                break;

                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GuestMenuscreenOptions();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);

                    break;

                    case "help":
                        logStatus = true;
                        Thread.Sleep(wait);

                        Help(); // This calls the support page.

                        Thread.Sleep(wait);

                        invalid = false;

                    break;

                    case "l":
                        Thread.Sleep(wait);
                        Console.Clear();

                        roleId = 0;
                        accountId = 0;
                        storageManager.CloseConnection();
                        StartMenuscreenOptions();

                        invalid = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        GuestMenuscreenOptions();

                        invalid = true;

                    break;

                }

            } while (invalid);

        }

        private static void GoBack() // The Go Back function. 
        {
            bool carry = true;


            do
            { 
                Thread.Sleep(wait);
                 Console.Clear();
               

                 switch (roleId)
                 {
                     case 1:

                         GuestMenuscreenOptions();

                            carry = false;

                     break;

                     case 2:

                         AdminMenuscreenOptions();

                            carry = false;

                     break;

                 }

      
            } while (carry);

        } 


        // The data-modifying commands for the Bands table.
        private static void UpdateBandName()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int bandId = view.GetIntInput();

                view.DisplayMessage("\nRename the record... ");
                string bandName = view.GetInput();

                int rowsAffected = storageManager.UpdateBandById(bandId, bandName);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateBandName();
    
            }

        }  

        private static void InsertNewBand()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new band... ");
                string bandName = view.GetInput();
                int bandId = 0; 

                Bands newBand = new Bands(bandId, bandName);

                int generatedId = storageManager.InsertBand(newBand);
                view.DisplayMessage($"\nThe new bands identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewBand();
    
            }

        }

        private static void DeleteBandById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int bandId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteBandById(bandId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteBandById();
    
            }

        }


        // The data-modifying commands for the Genres table.
        private static void UpdateGenreName()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Genres> genres = storageManager.GetAllGenres();
                int genreId = view.GetIntInput();

                view.DisplayMessage("\nRename the record... ");
                string genreName = view.GetInput();

                int rowsAffected = storageManager.UpdateGenreById(genreId, genreName);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateGenreName();
    
            }

        }

        private static void InsertNewGenre()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new genre... ");
                string genreName = view.GetInput();
                List<Genres> genres = storageManager.GetAllGenres();
                int genreId = 0; 

                Genres newGenre = new Genres(genreId, genreName);

                int generatedId = storageManager.InsertGenre(newGenre);
                view.DisplayMessage($"\nThe new genres identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewGenre();
    
            }

        }

        private static void DeleteGenreById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Genres> genres = storageManager.GetAllGenres();
                int genreId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteGenreById(genreId);
                view.DisplayMessage($"\nDeleted {rowsAffected} rows.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteGenreById();
    
            }

        }


        // The data-modifying commands for the Formats table.
        private static void UpdateFormatName()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();
                int formatId = view.GetIntInput();

                view.DisplayMessage("\nRename the record... ");
                string formatName = view.GetInput();

                int rowsAffected = storageManager.UpdateFormatById(formatId, formatName);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateFormatName();
    
            }

        }

        private static void InsertNewFormat()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new format... ");
                string formatName = view.GetInput();
                int formatId = 0; 

                Formats newFormat = new Formats(formatId, formatName);

                int generatedId = storageManager.InsertFormat(newFormat);
                view.DisplayMessage($"\nThe new formats identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewFormat();
    
            }

        }

        private static void DeleteFormatById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();
                int formatId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteFormatById(formatId);
                view.DisplayMessage($"\nDeleted {rowsAffected} rows.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteFormatById();
    
            }
        }


        // The data-modifying commands for the Artists table.
        private static void UpdateArtistName()
        {
            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Artists> artists = storageManager.GetAllArtists();
                int artistId = view.GetIntInput();

                view.DisplayMessage("\nRename the record... ");
                string artistName = view.GetInput();

                int rowsAffected = storageManager.UpdateArtistById(artistId, artistName);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateArtistName();
                
            }



        }

        private static void InsertNewArtist()
        {
            try
            {
                view.DisplayMessage("\nEnter the new artist... ");
                string artistName = view.GetInput();
                int artistId = 0;

                Artists newArtist = new Artists(artistId, artistName);

                int generatedId = storageManager.InsertArtist(newArtist);
                view.DisplayMessage($"\nThe new artists identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewArtist();
                
            }           

        }

        private static void DeleteArtistById()
        {
            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Artists> artists = storageManager.GetAllArtists();
                int artistId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteArtistById(artistId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");


            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteArtistById();
               
            }
        }


        // The data-modifying commands for the Rooms table.
        private static void UpdateRoomName()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Rooms> rooms = storageManager.GetAllRooms();
                int roomId = view.GetIntInput();

                view.DisplayMessage("\nRename the record... ");
                string roomName = view.GetInput();

                int rowsAffected = storageManager.UpdateRoomById(roomId, roomName);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateRoomName();

            }

        }

        private static void InsertNewRoom()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new room... ");
                string roomName = view.GetInput();
                int roomId = 0;

                Rooms newRoom = new Rooms(roomId, roomName);

                int generatedId = storageManager.InsertRoom(newRoom);
                view.DisplayMessage($"\nThe new rooms identification number is: {generatedId}");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewRoom();

            }

        }

        private static void DeleteRoomById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Rooms> rooms = storageManager.GetAllRooms();
                int roomId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteRoomById(roomId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteRoomById();

            }

        }


        // The data-modifying commands for the Shelves table.
        private static void UpdateShelfRoom()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();
                int shelfTagId = view.GetIntInput();

                view.DisplayMessage("\nEnter the identification number... ");
                int roomId = view.GetIntInput();

                int rowsAffected = storageManager.UpdateShelfRoomById(shelfTagId, roomId);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateShelfRoom();

            }

        }

        private static void InsertNewShelf()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new shelves tag... ");
                char shelfTag = view.GetCharInput();

                view.DisplayMessage("\nEnter the new room... ");
                int roomId = view.GetIntInput();
                int shelfTagId = 0;

                Shelves newShelf = new Shelves(shelfTagId, shelfTag, roomId);

                int generatedId = storageManager.InsertShelf(newShelf);
                view.DisplayMessage($"\nThe new shelves identification number is: {generatedId}");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertNewShelf();

            }

        }

        private static void DeleteShelfById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();
                int shelfTagId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteShelfById(shelfTagId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteShelfById();

            }

        }


        // The data-modifying commands for the accounts.
        private static void CreateUser()
        {
            

            try
            {
                view.DisplayMessage("\nEnter your first name... ");
                view.DisplayMessage(" ");
                string fName = view.GetInput();
                int personId = 0;

                view.DisplayMessage("\nEnter your last name... ");
                view.DisplayMessage(" ");
                string sName = view.GetInput();

                view.DisplayMessage("\nCreate a username... ");
                view.DisplayMessage(" ");
                string newuser = view.GetInput();
                roleId = 1;

                view.DisplayMessage("\nCreate a password... ");
                view.DisplayMessage(" ");
                string newpw = view.GetInput();

                Accounts newUser = new Accounts(personId, fName, sName, newuser, newpw, roleId);


                int generatedId = storageManager.CreateAccount(newUser);

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                CreateUser();

            }

        }

        private static void CreateAdmin()
        {
            

            try
            {
                view.DisplayMessage("\nEnter your first name... ");
                view.DisplayMessage(" ");
                string fName = view.GetInput();
                int personId = 0;

                view.DisplayMessage("\nEnter your last name... ");
                view.DisplayMessage(" ");
                string sName = view.GetInput();

                view.DisplayMessage("\nCreate a username... ");
                view.DisplayMessage(" ");
                string newuser = view.GetInput();
                roleId = 2;

                view.DisplayMessage("\nCreate a password... ");
                view.DisplayMessage(" ");
                string newpw = view.GetInput();

                Accounts newAdmin = new Accounts(personId, fName, sName, newuser, newpw, roleId);


                int generatedId = storageManager.CreateAccount(newAdmin);

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                CreateAdmin();

            }

        }


        // The data-modifying commands for the Artist Albums table.
        private static void UpdateArtistAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number... ");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();
                int albumId = view.GetIntInput();
            
                view.DisplayMessage("\nEnter the new album name... ");
                albums = storageManager.GetAllArtistAlbums();
            
                string albumName = view.GetInput();

                view.DisplayMessage("\nEnter a new genre identification number...");
                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int genreId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new date of release... YYYY/MM/DD");
                DateTime dateOfRelease = view.GetDateTimeInput();

                view.DisplayMessage("\nEnter a new format identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();

                int formatId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new artist identification number... ");
                List<Artists> artists = storageManager.GetAllArtists();
                view.DisplayArtists(artists);

                int artistId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new shelf row identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();

                int shelfRowId = view.GetIntInput();
  
                bool lost = false;

                int rowsAffected = storageManager.UpdateArtistAlbumById(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId, lost);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateArtistAlbum();

            }

        }
        
        private static void InsertArtistAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new album... ");
                string albumName = view.GetInput();
                int albumId = 0;

                view.DisplayMessage("\nEnter a genre identification number...");
                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int genreId = view.GetIntInput();

                view.DisplayMessage("\nEnter the date of release... YYYY/MM/DD");
                DateTime dateOfRelease = view.GetDateTimeInput();

                view.DisplayMessage("\nEnter the format identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();

                int formatId = view.GetIntInput();

                view.DisplayMessage("\nEnter the artist identification number... ");
                List<Artists> artists = storageManager.GetAllArtists();
                view.DisplayArtists(artists);

                int artistId = view.GetIntInput();

                view.DisplayMessage("\nEnter the shelf rows identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();

                int shelfRowId = view.GetIntInput();
  
                bool lost = false;

                ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId, lost);

                int generatedId = storageManager.InsertArtistAlbum(newAlbum);
                view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertArtistAlbum();

            }
            
        }
        
        private static void DeleteArtistAlbumById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int albumId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteArtistAlbumById(albumId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteArtistAlbumById();

            }

        }

        private static void MarkArtistAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int artistId = view.GetIntInput();

                bool lost = true;

                int rowsAffected = storageManager.LostArtist(artistId, lost);
                view.DisplayMessage($"\nMarked {rowsAffected} records as lost.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                MarkArtistAsLost();

            }

        }


        // The data-modifying commands for the Band Albums table.
        private static void UpdateBandAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number... ");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();
                int albumId = view.GetIntInput();
            
                view.DisplayMessage("\nEnter the new album name... ");
                albums = storageManager.GetAllBandAlbums();
            
                string albumName = view.GetInput();

                view.DisplayMessage("\nEnter a new genre identification number...");
                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int genreId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new date of release... YYYY/MM/DD");
                DateTime dateOfRelease = view.GetDateTimeInput();

                view.DisplayMessage("\nEnter a new format identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();

                int formatId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new band identification number... ");
                List<Bands> bands = storageManager.GetAllBands();
                view.DisplayBands(bands);

                int bandId = view.GetIntInput();

                view.DisplayMessage("\nEnter a new shelf row identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();

                int shelfRowId = view.GetIntInput();
  
                bool lost = false;

                int rowsAffected = storageManager.UpdateBandAlbumById(albumId, albumName, genreId, dateOfRelease, formatId, bandId, shelfRowId, lost);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateBandAlbum();

            }

        }
        
        private static void InsertBandAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new album... ");
                string albumName = view.GetInput();
                int albumId = 0;

                view.DisplayMessage("\nEnter a genre identification number...");
                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int genreId = view.GetIntInput();

                view.DisplayMessage("\nEnter the date of release... YYYY/MM/DD");
                DateTime dateOfRelease = view.GetDateTimeInput();

                view.DisplayMessage("\nEnter the format identification number... ");
                List<Formats> formats = storageManager.GetAllFormats();

                int formatId = view.GetIntInput();

                view.DisplayMessage("\nEnter the band identification number... ");
                List<Bands> bands = storageManager.GetAllBands();
                view.DisplayBands(bands);

                int bandId = view.GetIntInput();

                view.DisplayMessage("\nEnter the shelf rows identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();

                int shelfRowId = view.GetIntInput();
  
                bool lost = false;

                BandAlbums newAlbum = new BandAlbums(albumId, albumName, genreId, dateOfRelease, formatId, bandId, shelfRowId, lost);

                int generatedId = storageManager.InsertBandAlbum(newAlbum);
                view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertBandAlbum();

            }
            
        }
        
        private static void DeleteBandAlbumById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int albumId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteBandAlbumById(albumId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteBandAlbumById();

            }

        }

        private static void MarkBandAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int bandId = view.GetIntInput();

                bool lost = true;

                int rowsAffected = storageManager.LostBand(bandId, lost);
                view.DisplayMessage($"\nMarked {rowsAffected} records as lost.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                MarkBandAsLost();

            }

        }


        // The data-modifying commands for the Artist Albums Reviews table.
        private static void UpdateArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                List<ArtistReviews> reviews = storageManager.GetAllArtistReviews(accountId);

                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();

                int albumId = view.GetIntInput();

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                int rowsAffected = storageManager.UpdateArtistReviewById(reviewId, albumId, personId, tierId, favourite);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateArtistReview();

                
            }

        }
        
        private static void InsertArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number...");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();

                int albumId = view.GetIntInput();
                int reviewId = 0;

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                ArtistReviews newReviews = new ArtistReviews(reviewId, albumId, personId, tierId, favourite);

                int generatedId = storageManager.InsertArtistReview(newReviews);
                view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertArtistReview();
                
            }

        }
        
        private static void DeleteArtistReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<ArtistReviews> reviews = storageManager.GetAllArtistReviews(accountId);
                int reviewId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteArtistReviewById(reviewId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteArtistReviewById();
                
            }

        }

        private static void FavouriteArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<ArtistReviews> reviews = storageManager.GetAllArtistReviews(accountId);
                int reviewId = view.GetIntInput();

                bool favourite = true;

                int rowsAffected = storageManager.FavouriteArtist(reviewId, favourite);
                view.DisplayMessage($"\nMarked {rowsAffected} records as a favourite.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                FavouriteArtistReview();
                
            }

        }


        // The data-modifying commands for the Band Albums Reviews table.
        private static void UpdateBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                List<BandReviews> reviews = storageManager.GetAllBandReviews(accountId);

                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();

                int albumId = view.GetIntInput();

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                int rowsAffected = storageManager.UpdateBandReviewById(reviewId, albumId, personId, tierId, favourite);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                UpdateBandReview();

                
            }

        }
        
        private static void InsertBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number...");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();

                int albumId = view.GetIntInput();
                int reviewId = 0;

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                BandReviews newReviews = new BandReviews(reviewId, albumId, personId, tierId, favourite);

                int generatedId = storageManager.InsertBandReview(newReviews);
                view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                InsertBandReview();
                
            }

        }
        
        private static void DeleteBandReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<BandReviews> reviews = storageManager.GetAllBandReviews(accountId);
                int reviewId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteBandReviewById(reviewId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                DeleteBandReviewById();
                
            }

        }

        private static void FavouriteBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<BandReviews> reviews = storageManager.GetAllBandReviews(accountId);
                int reviewId = view.GetIntInput();

                bool favourite = true;

                int rowsAffected = storageManager.FavouriteBand(reviewId, favourite);
                view.DisplayMessage($"\nMarked {rowsAffected} records as a favourite.");

            }
            catch (FormatException e)
            {
                Console.WriteLine("\n  Please use the proper formatting.");
                Console.WriteLine(e.Message);

                FavouriteBandReview();
                
            }

        }


    }

}
