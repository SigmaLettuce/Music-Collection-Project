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
        private static StorageManager storageManager;
        private static ConsoleView view;

        static int accountId;
        static int roleId;

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
            GuestMenuscreenOptions();


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

            string input = view.DisplayHelp();
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

                        StartMenuscreenOptions();

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
                                    List<ArtistAlbums> Aalbums = storageManager.GetAllArtistAlbums(accountId);
                                    view.DisplayArtistAlbums(Aalbums);

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
                                                //Insert

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

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    List<BandAlbums> Balbums = storageManager.GetAllBandAlbums(accountId);
                                    view.DisplayBandAlbums(Balbums);

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

                    case "rooms":
                        List<Rooms> rooms = storageManager.GetAllRooms();
                        view.DisplayRooms(rooms);

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
                        List<Shelves> shelves = storageManager.GetAllShelves();
                        view.DisplayShelves(shelves);

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

                        select = view.DisplayEditingOptions("albums", "account~variants");

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

                        Thread.Sleep(wait);

                        Help(); // This calls the support page.

                        Thread.Sleep(wait);

                        invalid = false;

                    break;

                    case "l":
                        Thread.Sleep(wait);
                        Console.Clear();

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


        private static void GuestMenuscreenOptions() // The Admin Menuscreen. 
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
                                    List<ArtistAlbums> Aalbums = storageManager.GetAllArtistAlbums(accountId);
                                    view.DisplayArtistAlbums(Aalbums);

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
                                                //Insert

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

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    List<BandAlbums> Balbums = storageManager.GetAllBandAlbums(accountId);
                                    view.DisplayBandAlbums(Balbums);

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

                        select = view.DisplayEditingOptions("albums", "account~variants");

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

                        Thread.Sleep(wait);

                        Help(); // This calls the support page.

                        Thread.Sleep(wait);

                        invalid = false;

                    break;

                    case "l":
                        Thread.Sleep(wait);
                        Console.Clear();

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
                        Thread.Sleep(wait);
                        Console.Clear();
                        carry = false;

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

                    case "n":
                        GoBack();

                        carry = true;

                    break;

                    default:
                        view.DisplayError(wait);

                        GoBack();

                        carry = true;

                    break;

                }

            while (carry);

        } // The Go Back function.


        // The data-modifying commands for the Bands table.
        private static void UpdateBandName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int bandId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string bandName = view.GetInput();

            int rowsAffected = storageManager.UpdateBandById(bandId, bandName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }  

        private static void InsertNewBand()
        {
            view.DisplayMessage("\nEnter the new band... ");
            string bandName = view.GetInput();
            int bandId = 0; 

            Bands newBand = new Bands(bandId, bandName);

            int generatedId = storageManager.InsertBand(newBand);
            view.DisplayMessage($"\nThe new bands identification number is: {generatedId}");

        }

        private static void DeleteBandById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int bandId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteBandById(bandId);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");
        }


        // The data-modifying commands for the Genres table.
        private static void UpdateGenreName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int genreId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string genreName = view.GetInput();

            int rowsAffected = storageManager.UpdateGenreById(genreId, genreName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        private static void InsertNewGenre()
        {
            view.DisplayMessage("\nEnter the new genre... ");
            string genreName = view.GetInput();
            int genreId = 0; 

            Genres newGenre = new Genres(genreId, genreName);

            int generatedId = storageManager.InsertGenre(newGenre);
            view.DisplayMessage($"\nThe new bands identification number is: {generatedId}");

        }

        private static void DeleteGenreById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int genreId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteGenreById(genreId);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");
        }


        // The data-modifying commands for the Artists table.
        private static void UpdateArtistName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int artistId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string genreName = view.GetInput();

            int rowsAffected = storageManager.UpdateArtistById(artistId, genreName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        private static void InsertNewArtist()
        {
            view.DisplayMessage("\nEnter the new artist... ");
            string artistName = view.GetInput();
            int artistId = 0;

            Artists newArtist = new Artists(artistId, artistName);

            int generatedId = storageManager.InsertArtist(newArtist);
            view.DisplayMessage($"\nThe new artists identification number is: {generatedId}");

        }

        private static void DeleteArtistById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int artistId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteArtistById(artistId);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");
        }


        // The data-modifying commands for the Rooms table.
        private static void UpdateRoomName()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int roomId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomById(roomId, roomName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        private static void InsertNewRoom()
        {
            view.DisplayMessage("\ntEnter the new room... ");
            string roomName = view.GetInput();
            int roomId = 0;

            Rooms newRoom = new Rooms(roomId, roomName);

            int generatedId = storageManager.InsertRoom(newRoom);
            view.DisplayMessage($"\nThe new rooms identification number is: {generatedId}");

        }

        private static void DeleteRoomById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int roomId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteRoomById(roomId);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");

        }

        // The data-modifying commands for the Shelves table.
        private static void UpdateShelfRoom()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            char shelfTag = view.GetCharInput();

            view.DisplayMessage("\nEnter the identification number... ");
            int roomId = view.GetIntInput();

            int rowsAffected = storageManager.UpdateShelfRoomByTag(shelfTag, roomId);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        private static void InsertNewShelf()
        {
            view.DisplayMessage("\ntEnter the new room... ");
            string roomName = view.GetInput();
            int roomId = 0;

            Rooms newRoom = new Rooms(roomId, roomName);

            int generatedId = storageManager.InsertRoom(newRoom);
            view.DisplayMessage($"\nThe new rooms identification number is: {generatedId}");

        }

        private static void DeleteShelfByTag()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            char shelfTag = view.GetCharInput();

            int rowsAffected = storageManager.DeleteShelfRoomByTag(shelfTag);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");

        }

        // The data-modifying commands for the accounts.
        private static void CreateUser()
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

        private static void CreateAdmin()
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


        // The data-modifying commands for the Artist Albums table.
        private static void UpdateArtistAlbum()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int albumId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string albumName = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomById(albumId, albumName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        /*
        private static void InsertArtistAlbum()
        {
            view.DisplayMessage("\nEnter the new album... ");
            string albumName = view.GetInput();
            int albumId = 0;

            view.DisplayMessage("\nEnter a genre identification number...");
            List<Genres> genres = storageManager.GetAllBands();
            view.DisplayBands(bands);

            string genreName = view.GetInput();

            view.DisplayMessage("\nEnter the date of release... YYYY/MM/DD");
            DateTime dateOfRelease = view.GetDateTimeInput();

            view.DisplayMessage("\nEnter the format... ");
            string formatName = view.GetInput();

            view.DisplayMessage("\nEnter the artist... ");
            string artistName = view.GetInput();

            view.DisplayMessage("\nEnter the room its kept in... ");
            string roomName = view.GetInput();

            view.DisplayMessage("\nEnter the shelves tag letter... ");
            char shelfTag = view.GetCharInput();

            view.DisplayMessage("\nEnter the shelves row, accompanied by the tag letter... ");
            string shelfRow = view.GetInput();
            bool lost = false;

            ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, lost);

            int generatedId = storageManager.InsertArtistAlbum(newAlbum);
            view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");
            
        }
        */

        private static void DeleteArtistAlbumById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int albumId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteRoomById(albumId);
            view.DisplayMessage($"\n\tDeleted {rowsAffected} row.");
        }

        // The data-modifying commands for the Band Albums table.
        private static void UpdateBandAlbum()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int albumId = view.GetIntInput();

            view.DisplayMessage("\nRename the record... ");
            string albumName = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomById(albumId, albumName);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        /*
        private static void InsertBandAlbum()
        {
            view.DisplayMessage("\nEnter the new album... ");
            string albumName = view.GetInput();
            int albumId = 0;

            view.DisplayMessage("\nEnter the genre... ");
            string genreName = view.GetInput();

            view.DisplayMessage("\nEnter the date of release... ");
            DateTime dateOfRelease = view.GetDateTimeInput();

            view.DisplayMessage("\nEnter the format... ");
            string formatName = view.GetInput();

            view.DisplayMessage("\nEnter the artist... ");
            string artistName = view.GetInput();

            view.DisplayMessage("\nEnter the room its kept in... ");
            string roomName = view.GetInput();

            view.DisplayMessage("\nEnter the shelves tag letter... ");
            char shelfTag = view.GetCharInput();

            view.DisplayMessage("\nEnter the shelves row, accompanied by the tag letter... ");
            string shelfRow = view.GetInput();
            bool lost = false;

            ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow, lost);

            int generatedId = storageManager.InsertArtistAlbum(newAlbum);
            view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

        }
        */

        private static void DeleteBandAlbumById()
        {
            view.DisplayMessage("\nEnter the identification number... ");
            int albumId = view.GetIntInput();

            int rowsAffected = storageManager.DeleteRoomById(albumId);
            view.DisplayMessage($"\n\tDeleted {rowsAffected} row.");
        }

    }

}
