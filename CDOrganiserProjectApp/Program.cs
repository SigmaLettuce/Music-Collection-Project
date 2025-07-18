using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;


namespace CDOrganiserProjectApp
{
    public class Program
    {

        /*                   *\  
         
               [PROGRAM]
         
        \*                   */
        
        private static StorageManager storageManager; 
        private static ConsoleView view;
        
        static int accountId; // This is the variable that stores an Account ID; Declared as a static variable so it is accessible across all instances
        static int roleId;
        static bool logStatus;
        
        const int wait = 1000; 

        
        static void Main(string[] args)
        {
            // view.DisplayMessage("Hello, World!");

            view = new ConsoleView();

            StartMenuscreenOptions();

        }

        private static void StartMenuscreenOptions()
        {

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HomeMusicCollectionDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            storageManager = new StorageManager(connectionString);

            

            bool invalid = true; // A variable that evaluates the continuation of a process.


            string input = view.StartMenu(); // Calls the display, and returns a prompt for input.
            view.DisplayMessage("");
             
            do
            {
                switch (input.ToUpper())
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

            logStatus = false;
            CreateUser();
            Thread.Sleep(wait);
            Console.Clear();


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

            // Searches for matches for credentials in the database, the 

            string fetchUsername = storageManager.ScanUsername(pw);
            string fetchPassword = storageManager.ScanPassword(user);
            accountId = storageManager.FetchAccount(accountId);
            roleId = storageManager.FetchRole(user);


            // If the given credentials matches with any searches, the user is granted permission, depending on their role for how much can be visible.

            if (user.Equals(fetchUsername) && pw.Equals(fetchPassword))
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

                    case 3:
                        Thread.Sleep(wait);
                        Console.Clear();

                        TrueAdminMenuscreenOptions();

                    break;

                }
            }

            else
            {
                view.DisplayMessage("\nEither your username or password are incorrect. ");
                Thread.Sleep(wait);
                Console.Clear();

                StartMenuscreenOptions();
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
            
        } // The Help method. Calls the support page display.
         
        private static void TrueAdminMenuscreenOptions() // The True Admin Menuscreen. 
        {           
            string select; // Stores a selection from a listing that branches into multiple.
            int recordselect; // Stores a selection of a record.
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

                        cmd = view.DisplayEditingOptions("bands", "default~extras~search");
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

                                case "search":
                                    SearchBands();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                  
                    case "artists":
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("artists", "default~extras~search");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "search":
                                    SearchArtists();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

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

                                        switch (cmd.ToLower())
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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("artists", "artists");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetArtistReleaseDate();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetAToJArtists();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:
                                                            storageManager.GetArtistsEarly2000sMusic();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 4:
                                                            storageManager.GetTotalArtistGenres();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 5:
                                                            storageManager.GetArtistsAsAWhole();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 6:
                                                            storageManager.GetTotalAlbumsPublishedByArtists();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 7:
                                                            storageManager.GetTotalPublishingsOfAllArtistsPerYear();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 8:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchArtistAlbums();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

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

                                                GoBack();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("bands", "bands");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetBandReleaseDate();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetAToJBands();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:
                                                            storageManager.GetBandsEarly2000sMusic();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 4:
                                                            storageManager.GetTotalBandGenres();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 5:
                                                            storageManager.GetBandsAsAWhole();

                                                            cmd = view.GetInput();

                                                           do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 6:
                                                            storageManager.GetTotalAlbumsPublishedByBands();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 7:
                                                            storageManager.GetTotalPublishingsOfAllBandsPerYear();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 8:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchBandAlbums();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

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

                                                GoBack();

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

                                    GoBack();

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
                                    storageManager.GetAllArtistReviews();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("artists", "reviews");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetHighRankingArtistAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetTopThreeFavouriteArtistAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchArtistReviews();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GoBack();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandReviews();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("bands", "reviews");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetHighRankingBandAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetTopThreeFavouriteBandAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchBandReviews();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GoBack();

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

                                    GoBack();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);
                    break;

                    case "genres":
                        List<Genres> genres = storageManager.GetAllGenres();
                        view.DisplayGenres(genres);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("genres", "default~extras~search");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "search":
                                    SearchGenres();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

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

                            switch (cmd.ToLower())
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

                                    GoBack();

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

                            switch (cmd.ToLower())
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

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "shelves":
                        storageManager.GetAllShelves();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("shelves", "default~extras~view");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "reports":

                                    recordselect = view.DisplayRecordOptions("shelves", "reviews");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {
                                        switch (recordselect)
                                        {
                                            case 1:
                                                storageManager.GetTotalOfRowOccupancyPerShelf();

                                                cmd = view.GetInput();

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);
    

                                            break;

                                            case 2:                                   
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

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

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "rows":
                        storageManager.GetAllRows();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("rows", "default~extras~view");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
                            {
                                case "up":
                                    UpdateRow();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewRow();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteRowById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "reports":

                                    recordselect = view.DisplayRecordOptions("rows", "reviews");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {
                                        switch (recordselect)
                                        {
                                            case 1:
                                                storageManager.GetTotalOfRowOccupancyPerShelf();

                                                cmd = view.GetInput();

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);
    

                                            break;

                                            case 2:                                   
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

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

                                    GoBack();

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

                                    GoBack();

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
                            switch (select.ToLower())
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

                                    GoBack();

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

                        GoBack();

                        invalid = true;

                    break;

                }


            } while (invalid);

        }

        private static void AdminMenuscreenOptions() // The Admin Menuscreen. 
        {           
            string select; // Stores a selection from a listing that branches into multiple.
            int recordselect; // Stores a selection of a record.
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

                        cmd = view.DisplayEditingOptions("bands", "default~extras~search");
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

                                case "search":
                                    SearchBands();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;
                  
                    case "artists":
                        List<Artists> artists = storageManager.GetAllArtists();
                        view.DisplayArtists(artists);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("artists", "default~extras~search");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "search":
                                    SearchArtists();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                    GoBack();

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

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

                                        switch (cmd.ToLower())
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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("artists", "artists");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetArtistReleaseDate();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetAToJArtists();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:
                                                            storageManager.GetArtistsEarly2000sMusic();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 4:
                                                            storageManager.GetTotalArtistGenres();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 5:
                                                            storageManager.GetArtistsAsAWhole();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 6:
                                                            storageManager.GetTotalAlbumsPublishedByArtists();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 7:
                                                            storageManager.GetTotalPublishingsOfAllArtistsPerYear();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 8:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchArtistAlbums();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

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

                                                GoBack();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("bands", "bands");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetBandReleaseDate();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetAToJBands();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:
                                                            storageManager.GetBandsEarly2000sMusic();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 4:
                                                            storageManager.GetTotalBandGenres();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 5:
                                                            storageManager.GetBandsAsAWhole();

                                                            cmd = view.GetInput();

                                                           do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 6:
                                                            storageManager.GetTotalAlbumsPublishedByBands();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 7:
                                                            storageManager.GetTotalPublishingsOfAllBandsPerYear();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 8:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchBandAlbums();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

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

                                                GoBack();

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

                                    GoBack();

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
                                    storageManager.GetAllArtistReviews();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("artists", "reviews");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetHighRankingArtistAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetTopThreeFavouriteArtistAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchArtistReviews();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GoBack();

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandReviews();

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

                                            case "reports":

                                                recordselect = view.DisplayRecordOptions("bands", "reviews");

                                                Thread.Sleep(wait);
                                                Console.Clear();

                                                invalid = false;

                                                do
                                                {
                                                    switch (recordselect)
                                                    {
                                                        case 1:
                                                            storageManager.GetHighRankingBandAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 2:
                                                            storageManager.GetTopThreeFavouriteBandAlbums();

                                                            cmd = view.GetInput();

                                                            do
                                                            {
                                                                switch (cmd.ToUpper())
                                                                {
                                                                    case "E":
                                                                        GoBack();

                                                                        invalid = false;

                                                                    break;

                                                                    default:
                                                                        view.AltDisplayError(wait);

                                                                        GoBack();

                                                                        invalid = true;

                                                                    break;

                                                                }

                                                            } while (invalid);
                                                

                                                        break;

                                                        case 3:                                   
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);

                                            break;

                                            case "search":
                                                SearchBandReviews();

                                                cmd = view.GetInput();
                                                view.DisplayMessage("");

                                                Thread.Sleep(wait);

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                }  while (invalid);

                                                invalid = false;

                                                GoBack();

                                            break;

                                            case "back":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.DisplayError(wait);

                                                GoBack();

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

                                    GoBack();

                                    invalid = true;

                                break;

                            }

                        } while (invalid);
                    break;

                    case "genres":
                        List<Genres> genres = storageManager.GetAllGenres();
                        view.DisplayGenres(genres);

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("genres", "default~extras~search");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "search":
                                    SearchGenres();

                                    cmd = view.GetInput();
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);

                                    do
                                    {
                                        switch (cmd.ToUpper())
                                        {
                                            case "E":
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

                                            break;

                                        }

                                    }  while (invalid);

                                    invalid = false;

                                break;

                                case "back":                                   
                                    GoBack();

                                    invalid = false;

                                break;

                                default:
                                    view.DisplayError(wait);

                                    GoBack();

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

                            switch (cmd.ToLower())
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

                                    GoBack();

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

                            switch (cmd.ToLower())
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

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "shelves":
                        storageManager.GetAllShelves();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("shelves", "default~extras~view");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
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

                                case "reports":

                                    recordselect = view.DisplayRecordOptions("shelves", "reviews");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {
                                        switch (recordselect)
                                        {
                                            case 1:
                                                storageManager.GetTotalOfRowOccupancyPerShelf();

                                                cmd = view.GetInput();

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);
    

                                            break;

                                            case 2:                                   
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

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

                                    GoBack();

                                    invalid = true;

                                break;
                            }

                        } while (invalid);

                    break;

                    case "rows":
                        storageManager.GetAllRows();

                        Thread.Sleep(wait);

                        cmd = view.DisplayEditingOptions("rows", "default~extras~view");
                        view.DisplayMessage("");

                        Thread.Sleep(wait);

                        invalid = false;

                        do
                        {

                            switch (cmd.ToLower())
                            {
                                case "up":
                                    UpdateRow();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "ins":
                                    InsertNewRow();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "del":
                                    DeleteRowById();

                                    invalid = false;

                                    GoBack();

                                break;

                                case "reports":

                                    recordselect = view.DisplayRecordOptions("rows", "reviews");

                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    invalid = false;

                                    do
                                    {
                                        switch (recordselect)
                                        {
                                            case 1:
                                                storageManager.GetTotalOfRowOccupancyPerShelf();

                                                cmd = view.GetInput();

                                                do
                                                {
                                                    switch (cmd.ToUpper())
                                                    {
                                                        case "E":
                                                            GoBack();

                                                            invalid = false;

                                                        break;

                                                        default:
                                                            view.AltDisplayError(wait);

                                                            GoBack();

                                                            invalid = true;

                                                        break;

                                                    }

                                                } while (invalid);
    

                                            break;

                                            case 2:                                   
                                                GoBack();

                                                invalid = false;

                                            break;

                                            default:
                                                view.AltDisplayError(wait);

                                                GoBack();

                                                invalid = true;

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

                                    GoBack();

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

                                    GoBack();

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
                            switch (select.ToLower())
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

                                    GoBack();

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

                        GoBack();

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

                                    cmd = view.DisplayEditingOptions("artist-albums", "none");
                                    view.DisplayMessage("");

                                    Thread.Sleep(wait);
                                    Console.Clear();

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

                                            break;
                                        }

                                    } while (invalid);

                                    break;

                                case "bands":
                                    storageManager.GetAllBandAlbums();

                                    Thread.Sleep(wait);

                                    cmd = view.DisplayEditingOptions("band-albums", "none");
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
            // This is the 'go back' function; or method. It passes the role of the account that is currently logged on through a switch; the switch cases then go on to determine which menuscreen the user is booted to depending on their role.

                Thread.Sleep(wait);
                 Console.Clear();
               

                 switch (roleId)
                 {
                     case 1:
                    // If the user is simply a guest, the user gets sent back to the guest menu.

                         GuestMenuscreenOptions();


                     break;

                     case 2:
                    // If the user, however, is an admin, the user gets sent back to the admin menu instead.

                         AdminMenuscreenOptions();


                     break;

                    case 3:
                    // If the user is the very first person who installed the application, they get sent back to the 'true' admin menu.
                     
                        TrueAdminMenuscreenOptions();


                    break;

                 }


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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateBandName();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteBandById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateGenreName();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteGenreById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateFormatName();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteFormatById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateArtistName();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteArtistById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateRoomName();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteRoomById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
                List<Rooms> rooms = storageManager.GetAllRooms();
                int roomId = view.GetIntInput();

                int rowsAffected = storageManager.UpdateShelfRoomById(shelfTagId, roomId);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");
            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateShelfRoom();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertNewShelf();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteShelfById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteShelfById();

            }

        }


        // The data-modifying commands for the Rows table.
        private static void UpdateRow()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Rows> rows = storageManager.GetAllRows();
                int shelfRowId = view.GetIntInput();

                view.DisplayMessage("\nEnter the identification number... ");
                List<Shelves> shelves = storageManager.GetAllShelves();
                int shelfTagId = view.GetIntInput();

                int rowsAffected = storageManager.UpdateRowsShelfById(shelfRowId, shelfTagId);
                view.DisplayMessage($"\nUpdated {rowsAffected} records.");
            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateRow();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                UpdateRow();

            }

        }

        private static void InsertNewRow()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new rows number... ");
                int shelfRow = view.GetIntInput();

                view.DisplayMessage("\nEnter the shelf it belongs on... ");
                List<Shelves> shelves = storageManager.GetAllShelves();
                int shelfTagId = view.GetIntInput();
                int shelfRowId = 0;

                Rows newRow = new Rows(shelfRowId, shelfRow, shelfTagId);

                int generatedId = storageManager.InsertRow(newRow);
                view.DisplayMessage($"\nThe new shelves identification number is: {generatedId}");
            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertNewRow();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                InsertNewRow();

            }

        }

        private static void DeleteRowById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<Rows> rows = storageManager.GetAllRows();
                int shelfRowId = view.GetIntInput();

                int rowsAffected = storageManager.DeleteRowById(shelfRowId);
                view.DisplayMessage($"\nDeleted {rowsAffected} row.");
            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteRowById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteRowById();

            }

        }


        // The data-modifying commands for the accounts.
        private static void CreateUser()
        {
            

            try
            {
                logStatus = true;

                view.DisplayMessage("[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

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

                if (newuser.Equals(storageManager.FetchUsername(newpw)))
                {
                    view.DisplayMessage("\nThat username is already taken. Choose a different username. ");
                    Thread.Sleep(wait);
                    Console.Clear();

                    switch (logStatus)
                    {
                        case true:
                             GuestMenuscreenOptions();
                            
                        break;

                        case false:
                            StartMenuscreenOptions();

                        break;
                    }
                }

                else
                {
                    Accounts newUser = new Accounts(personId, fName, sName, newuser, newpw, roleId);


                    int generatedId = storageManager.CreateAccount(newUser);

                    view.DisplayMessage("\n  Successful. Booting you to the login/register.");
                    Thread.Sleep(wait);
                    Console.Clear();
                    StartMenuscreenOptions();

                }

                

            }

            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                CreateUser();

            }

        }

        private static void CreateAdmin()
        {
            

            try
            {
                logStatus = true;

                view.DisplayMessage("[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

               
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
                roleId = 2; // Assigns the Admninistrator role

                view.DisplayMessage("\nCreate a password... ");
                view.DisplayMessage(" ");
                string newpw = view.GetInput();

                if (newuser.Equals(storageManager.FetchUsername(newpw)))
                {
                    view.DisplayMessage("\nThat username is already taken. Choose a different username. ");
                    Thread.Sleep(wait);
                    Console.Clear();

                    switch (logStatus)
                    {
                        case true:
                             AdminMenuscreenOptions();
                            
                        break;

                        case false:
                            StartMenuscreenOptions();

                        break;
                    }
                }

                else
                {
                    Accounts newUser = new Accounts(personId, fName, sName, newuser, newpw, roleId);


                    int generatedId = storageManager.CreateAccount(newUser);

                    view.DisplayMessage("\n  Successful. Booting you to the login/register.");
                    Thread.Sleep(wait);
                    Console.Clear();
                    StartMenuscreenOptions();

                }

                

            }

            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateArtistAlbum();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertArtistAlbum();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteArtistAlbumById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteArtistAlbumById();

            }

        }

        private static void MarkArtistAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<ArtistAlbums> artists = storageManager.GetAllArtistAlbums();
                int artistId = view.GetIntInput();

                bool lost = true;

                    switch (storageManager.FetchLostFromArtistAlbums(lost))
                    {
                        case true:
                            lost = false;

                        break;

                        case false:
                            lost = true;

                        break;
                    }

                int rowsAffected = storageManager.LostArtist(artistId, lost);
                view.DisplayMessage($"\nMarked {rowsAffected} records as lost.");

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateBandAlbum();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertBandAlbum();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteBandAlbumById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteBandAlbumById();

            }

        }

        private static void MarkBandAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<BandAlbums> bands = storageManager.GetAllBandAlbums();
                int bandId = view.GetIntInput();

                bool lost = true;

                    switch (storageManager.FetchLostFromBandAlbums(lost))
                    {
                        case true:
                            lost = false;

                        break;

                        case false:
                            lost = true;

                        break;
                    }

                int rowsAffected = storageManager.LostBand(bandId, lost);
                view.DisplayMessage($"\nMarked/unmarked {rowsAffected} records as lost.");

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                MarkBandAsLost();

            }

        }


        // The data-modifying commands for the Artist Albums Reviews table.
        private static void UpdateArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);

                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();

                int albumId = view.GetIntInput();

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                

                if (personId.Equals(storageManager.FetchAccountFromArtistReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {
                    int rowsAffected = storageManager.UpdateArtistReviewById(reviewId, albumId, personId, tierId, favourite);
                    view.DisplayMessage($"\nUpdated {rowsAffected} records.");


                }

            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateArtistReview();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertArtistReview();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                InsertArtistReview();
                
            }

        }
        
        private static void DeleteArtistReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId.Equals(storageManager.FetchAccountFromArtistReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {
                    int rowsAffected = storageManager.DeleteArtistReviewById(reviewId);
                    view.DisplayMessage($"\nDeleted {rowsAffected} row.");

                }

            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteArtistReviewById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteArtistReviewById();
                
            }

        }

        private static void FavouriteArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);
                int reviewId = view.GetIntInput();

                

                if (accountId.Equals(storageManager.FetchAccountFromArtistReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {
                    
                    bool favourite = true;

                    switch (storageManager.FetchFavouriteFromArtistReviews(favourite))
                    {
                        case true:
                            favourite = false;

                        break;

                        case false:
                            favourite = true;

                        break;
                    }

                    int rowsAffected = storageManager.FavouriteArtist(reviewId, favourite);
                    view.DisplayMessage($"\nMarked/unmarked {rowsAffected} records as a favourite.");


                }

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                FavouriteArtistReview();
                
            }

        }


        // The data-modifying commands for the Band Albums Reviews table.
        private static void UpdateBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);

                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();

                int albumId = view.GetIntInput();

                view.DisplayMessage("\nEnter a ranking identification number...");
                List<Tiers> tiers = storageManager.GetAllTiers();
                int tierId = view.GetIntInput();

                int personId = accountId;
  
                bool favourite = false;

                if (personId.Equals(storageManager.FetchAccountFromBandReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {
                    int rowsAffected = storageManager.UpdateBandReviewById(reviewId, albumId, personId, tierId, favourite);
                    view.DisplayMessage($"\nUpdated {rowsAffected} records.");


                }

            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                UpdateBandReview();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

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
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                InsertBandReview();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                InsertBandReview();
                
            }

        }
        
        private static void DeleteBandReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId.Equals(storageManager.FetchAccountFromBandReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {
                    int rowsAffected = storageManager.DeleteBandReviewById(reviewId);
                    view.DisplayMessage($"Deleted {rowsAffected} records.");


                }

            }
            catch (IndexOutOfRangeException e)
            { 
                view.DisplayMessage("\n  Please enter a valid parameter listed. If you want to create a new listing, please navigate to such.");
                view.DisplayMessage(e.Message);

                DeleteBandReviewById();

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                DeleteBandReviewById();
                
            }

        }

        private static void FavouriteBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId.Equals(storageManager.FetchAccountFromBandReviews(reviewId)))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. Booting you back to the menuscreen.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    AdminMenuscreenOptions();

                }

                else
                {   
                    bool favourite = true;


                    switch (storageManager.FetchFavouriteFromBandReviews(favourite))
                    {
                        case true:
                            favourite = false;

                        break;

                        case false:
                            favourite = true;

                        break;
                    }

                    int rowsAffected = storageManager.FavouriteBand(reviewId, favourite);
                    view.DisplayMessage($"\nMarked/unmarked {rowsAffected} records as a favourite.");


                }
             

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                FavouriteBandReview();
                
            }

        }


        /*                                              *\  
         
               [UPDATES, INSERTS, DELETES AND MORE]
         
        \*                                              */

        /*
        
        UPDATE:

        All methods used to update a field
          
        */

        
        // The search options.
        private static void SearchGenres()
        {

            try
            {
                view.DisplayMessage("\nEnter the name... ");
                string search = view.GetInput();

                storageManager.SearchGenres(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchGenres();
    
            }

        }

        private static void SearchArtists()
        {

            try
            {
                view.DisplayMessage("\nEnter the name... ");
                string search = view.GetInput();

                storageManager.SearchArtists(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchArtists();
    
            }

        }

        private static void SearchBands()
        {

            try
            {
                view.DisplayMessage("\nEnter the name... ");
                string search = view.GetInput();

                storageManager.SearchBands(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchBands();
    
            }

        }

        private static void SearchBandReviews()
        {

            try
            {
                view.DisplayMessage("\nEnter the ranking... ");
                string search = view.GetInput();

                storageManager.SearchBandReviews(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchBandReviews();
    
            }

        }

        private static void SearchArtistReviews()
        {

            try
            {
                view.DisplayMessage("\nEnter the ranking... ");
                string search = view.GetInput();

                storageManager.SearchArtistReviews(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchArtistReviews();
    
            }

        }

        private static void SearchArtistAlbums()
        {

            try
            {
                view.DisplayMessage("\nEnter the name... ");
                string search = view.GetInput();

                storageManager.SearchArtistAlbums(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchArtistAlbums();
    
            }

        }

        private static void SearchBandAlbums()
        {

            try
            {
                view.DisplayMessage("\nEnter the name... ");
                string search = view.GetInput();

                storageManager.SearchBandAlbums(search);

            }
            catch (FormatException e)
            {
                view.DisplayMessage("\n  Please use the proper formatting.");
                view.DisplayMessage(e.Message);

                SearchBandAlbums();
    
            }

        }

    }

}
