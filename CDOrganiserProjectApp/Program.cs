using CDOrganiserProjectApp;
using CDOrganiserProjectApp.Index;
using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CDOrganiserProjectApp
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;
        const int wait = 1000;

        private static int roleId;

        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HomeMusicCollectionDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            storageManager = new StorageManager(connectionString);
            view = new ConsoleView();

            StartMenuscreenOptions();
            
            storageManager.CloseConnection();

        }

        private static void StartMenuscreenOptions()
        {

            bool invalid = true;

            string startInput = view.StartMenu();
            
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (startInput.ToUpper())
                {
                    case "R":
                        view.DisplayMessage("\nEnter your first name... ");
                        string fName = view.GetInput();
                        int personId = 0;

                        view.DisplayMessage("\nEnter your last name... ");
                        string sName = view.GetInput();

                        view.DisplayMessage("\nCreate a username... ");
                        string newuser = view.GetInput();
                        roleId = 1;

                        view.DisplayMessage("\nCreate a password... ");
                        string newpw = view.GetInput();

                        Accounts newVisitor = new Accounts(personId, fName, sName, newuser, newpw, roleId);
                        

                        int generatedId = storageManager.CreateAccount(newVisitor);
                        Thread.Sleep(wait);

                        GuestMenuscreenOptions();
                        
                        invalid = false;
                        
                    break;

                    case "L":
                        view.DisplayMessage("\nEnter your username... ");
                        string user = view.GetInput();


                        view.DisplayMessage("\nEnter your password... ");
                        string pw = view.GetInput();

                        string fetchuser = storageManager.GetUsernameCredentials(pw);
                        string fetchpw = storageManager.GetPasswordCredentials(user);
                        roleId = storageManager.FetchRole(pw);

                        invalid = false;

                        if (user.Equals(fetchuser) && pw.Equals(fetchpw))
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

                        else if (user != fetchuser && pw == fetchpw) 
                        {
                            view.DisplayMessage("Your username is incorrect. ");
                            Thread.Sleep(wait);
                            Console.Clear();

                            StartMenuscreenOptions();
                        }

                        else if (user == fetchuser && pw != fetchpw)
                        {
                            view.DisplayMessage("Your password is incorrect. ");
                            Thread.Sleep(wait);
                            Console.Clear();

                            StartMenuscreenOptions();
                        }

                        else if (user != fetchuser && pw != fetchpw)
                        {
                            view.DisplayMessage("Your username and password are incorrect. ");
                            Thread.Sleep(wait);
                            Console.Clear();

                            StartMenuscreenOptions();
                        }

                    break;

                    default:
                        view.DisplayMessage("I'm sorry, this isn't a valid selection. Can you try again? ");
                        Thread.Sleep(wait);
                        Console.Clear();

                        StartMenuscreenOptions();

                        invalid = true;

                    break;
                }

            }  while (invalid);



            
        }

        private static void AdminMenuscreenOptions()
        {
            
            bool invalid = true;

            string adminInput = view.DisplayAdminMenu();
            view.DisplayMessage("");

            Thread.Sleep(wait);


            do
            {
                switch (adminInput.ToLower())
                {

                    case Prefix.@view + " " + Suffix.@bands:
                        List<Bands> bands = storageManager.GetAllBands();
                        view.DisplayBands(bands);
        
                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@up + " " + Suffix.@bands:
                        UpdateBandName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@ins + " " + Suffix.@bands:
                        InsertNewBand();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.@del + " " + Suffix.@bands:
                        DeleteBandByName();

                        invalid = false;

                        GoBack();

                    break;

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

                    case Prefix.view + " " + Suffix.@artists:
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.up + " " + Suffix.@artists:
                        UpdateArtistName();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.ins + " " + Suffix.@artists:
                        InsertNewArtist();

                        invalid = false;

                        GoBack();

                    break;

                    case Prefix.del + " " + Suffix.@artists:
                        DeleteArtistByName();

                        invalid = false;

                        GoBack();

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



        private static void UpdateRoomName()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string roomName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomByName(roomName, rename);
            view.DisplayMessage($"Updated {rowsAffected} records.");

        }

        private static void InsertNewRoom()
        {
            view.DisplayMessage("\nEnter the new room... ");
            string roomName = view.GetInput();
            int roomId = 0;

            Rooms newRoom = new Rooms(roomId, roomName);

            int generatedId = storageManager.InsertRoom(newRoom);
            view.DisplayMessage($"The new rooms identification number is: {generatedId}");

        }
        private static void DeleteRoomByName()
        {
            view.DisplayMessage("Enter the room you wish to erase from your records... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.DeleteRoomByName(roomName);
            view.DisplayMessage($"Deleted {rowsAffected} row.");

        }

        private static void UpdateArtistAlbum()
        {
            view.DisplayMessage("\nEnter the name of the record... ");
            string albumName = view.GetInput();

            view.DisplayMessage("\nRename the record... ");
            string rename = view.GetInput();

            int rowsAffected = storageManager.UpdateRoomByName(albumName, rename);
            view.DisplayMessage($"Updated {rowsAffected} records.");

        }

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

            ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow, lost);

            int generatedId = storageManager.InsertArtistAlbum(newAlbum);
            view.DisplayMessage($"The new albums identification number is: {generatedId}");

        }
        private static void DeleteArtistAlbumByName()
        {
            view.DisplayMessage("Enter the room you wish to erase from your records... ");
            string roomName = view.GetInput();

            int rowsAffected = storageManager.DeleteRoomByName(roomName);
            view.DisplayMessage($"Deleted {rowsAffected} row.");
        }


    }

}
