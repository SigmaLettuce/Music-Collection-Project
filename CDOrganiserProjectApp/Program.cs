using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
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

        }

        private static void StartMenuscreenOptions()
        {

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HomeMusicCollectionDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            storageManager = new StorageManager(connectionString);

            bool invalid = true;


            string startInput = view.StartMenu();
            view.DisplayMessage("");
             
            do
            {
                switch (startInput.ToUpper())
                {
                    case "R":

                        Register();

                        invalid = false;

                    break;

                    case "L":

                        Login();

                        invalid = false;

                    break;

                    default:
                        view.DisplayError();

                        StartMenuscreenOptions();

                        invalid = true;

                    break;
                }

            }  while (invalid);



            
        }

        private static void PasswordEncryption()
        {
            
        }

        private static void Register()
        {
            Thread.Sleep(wait);
            Console.Clear();

            CreateUser();
            Thread.Sleep(wait);
            Console.Clear();
            GuestMenuscreenOptions();


        }

        private static void Login()
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

        private static void AdminMenuscreenOptions()
        {
            
            bool invalid = true;

            string input = view.DisplayAdminMenu();
            view.DisplayMessage("");

            Thread.Sleep(wait);


            do
            {
                switch (input.ToLower())
                {

                    case "bands":
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);

                        Thread.Sleep(wait);

                        string cmd = view.DisplayEditingOptions("bands");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd)
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
                                    DeleteBandByName();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":

                                    
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError();

                                break;
                            }

                        } while (invalid);

                    break;

                    /*

                    case Prefix.@view + " " + Suffix.@artists:
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@up + " " + Suffix.@artists:
                        UpdateArtistName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@ins + " " + Suffix.@artists:
                        InsertNewArtist();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@del + " " + Suffix.@artists:
                        DeleteArtistByName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@view + " " + Suffix.@all:
                        storageManager.GetAllArtistsAndBands();

                        invalid = false;

                        GoBack();

                        break;

                    case Prefix.@view + " " + Suffix.@rooms:
                        List<Rooms> rooms = storageManager.GetAllRooms();
                        view.DisplayRooms(rooms);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@up + " " + Suffix.@rooms:
                        UpdateRoomName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@ins + " " + Suffix.@rooms:
                        InsertNewRoom();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@del + " " + Suffix.@rooms:
                        DeleteRoomByName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@view + " " + Base.@artist + " " + Suffix.@albums:
                        List<ArtistAlbums> albumsa = storageManager.GetAllArtistAlbums();
                        view.DisplayArtistAlbums(albumsa);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@view + " " + Base.@band + " " + Suffix.@albums:
                        List<BandAlbums> albumsb = storageManager.GetAllBandAlbums();
                        view.DisplayBandAlbums(albumsb);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@ins + " " + Base.@band + " " + Suffix.@albums:
                        InsertBandAlbum();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@create + " " + Suffix.@admin:
                        CreateAdmin();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@create + " " + Suffix.@user:
                        CreateUser();

                        invalid = false;
                        
                        GoBack();

                    break;
                */
                    case "L":
                        Thread.Sleep(wait);
                        Console.Clear();

                        StartMenuscreenOptions();
                        storageManager.CloseConnection();

                        invalid = false;

                    break;

           

                    default:
                        view.DisplayError();

                        AdminMenuscreenOptions();

                        invalid = true;

                    break;

                }

            } while (invalid);

        }

        private static void GuestMenuscreenOptions()
        {

            bool invalid = true;

            string guestInput = view.DisplayGuestMenu();
            view.DisplayMessage("");

            Thread.Sleep(wait);


            do
            {
                switch (guestInput.ToLower())
                {
                    /*

                    case Prefix.@view + " " + Suffix.@categories:
                        Thread.Sleep(wait);
                        Console.Clear();

                        

                    break;

                    case Prefix.@log + " " + Suffix.@out:
                        Thread.Sleep(wait);
                        Console.Clear();

                        StartMenuscreenOptions();

                        invalid = false;

                    break;

                    default:
                        view.DisplayMessage("I'm sorry, this isn't a valid selection. Can you try again? ");
                        Thread.Sleep(wait);
                        Console.Clear();

                        GuestMenuscreenOptions();

                        invalid = true;

                    break;

                    */

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
                        view.DisplayError();

                        GoBack();

                        carry = true;

                    break;

                }

            while (carry);

        }


        /*
        private static void Update(char @class)
        {

            view.DisplayMessage("\nEnter the current name... ");
            string name = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected;

            switch (@class)
            {

                case 'A':

                    rowsAffected = storageManager.UpdateArtistByName(name, rename);
                    view.DisplayMessage($"\nUpdated {rowsAffected} record/s.");

                break;

                case 'B':

                    rowsAffected = storageManager.UpdateBandByName(name, rename);
                    view.DisplayMessage($"\nUpdated {rowsAffected} record/s.");

                break;

                case 'C':

                    rowsAffected = storageManager.UpdateRoomByName(name, rename);
                    view.DisplayMessage($"\nUpdated {rowsAffected} record/s.");

                break;

            }

        }
        */

        private static void UpdateBandName()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string bandName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateBandByName(bandName, rename);
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

        private static void DeleteBandByName()
        {
            view.DisplayMessage("\nEnter the band you wish to erase from your records... ");
            string bandName = view.GetInput();

            int rowsAffected = storageManager.DeleteBandByName(bandName);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");
        }


        private static void UpdateArtistName()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string artistName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateArtistByName(artistName, rename);
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

        private static void DeleteArtistByName()
        {
            view.DisplayMessage("\nEnter the artist you wish to erase from your records... ");
            string artistName = view.GetInput();

            int rowsAffected = storageManager.DeleteArtistByName(artistName);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");
        }


        private static void UpdateRoomName()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string roomName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomByName(roomName, rename);
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

        private static void DeleteRoomByName()
        {
            view.DisplayMessage("\nEnter the room you wish to erase from your records... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.DeleteRoomByName(roomName);
            view.DisplayMessage($"\nDeleted {rowsAffected} row.");

        }


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

        private static void UpdateArtistAlbum()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string albumName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomByName(albumName, rename);
            view.DisplayMessage($"\nUpdated {rowsAffected} records.");

        }

        /*
        private static void InsertArtistAlbum()
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

            ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, lost);

            int generatedId = storageManager.InsertArtistAlbum(newAlbum);
            view.DisplayMessage($"\nThe new albums identification number is: {generatedId}");

        }
        */

        private static void DeleteArtistAlbumByName()
        {
            view.DisplayMessage("\n\tEnter the room you wish to erase from your records... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.DeleteRoomByName(roomName);
            view.DisplayMessage($"\n\tDeleted {rowsAffected} row.");
        }


        private static void UpdateBandAlbum()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string albumName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomByName(albumName, rename);
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

        private static void DeleteBandAlbumByName()
        {
            view.DisplayMessage("\n\tEnter the room you wish to erase from your records... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.DeleteRoomByName(roomName);
            view.DisplayMessage($"\n\tDeleted {rowsAffected} row.");
        }

    }

}
