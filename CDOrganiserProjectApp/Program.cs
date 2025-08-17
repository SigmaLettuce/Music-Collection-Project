using CDOrganiserProjectApp.Model;
using CDOrganiserProjectApp.View;
using System.Collections;


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

        static int wait = 1500; // A globally shared integer for delays - absolves latency issues.

        
        static void Main(string[] args)
        {
            // view.DisplayMessage("Hello, World!");

            view = new ConsoleView();

            StartMenuscreenOptions();

        }

        private static void StartMenuscreenOptions()
        {

            string pathToMdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "New Database.mdf");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={pathToMdf};Integrated Security=True;Connect Timeout=30;";

            storageManager = new StorageManager(connectionString);

            logStatus = false;
            
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



            
        } // The Start method.

        private static void Register() // The Register method. Essentially an insert.
        {
            Thread.Sleep(wait);
            Console.Clear();

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
            accountId = storageManager.FetchAccount(user);
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

            string helpInput = view.DisplayHelp(wait);
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (helpInput.ToUpper())
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
        

        private static void AdminMenuscreenOptions() // The Admin Menuscreen. 
        {           
            bool invalid = true;

            string adminInput = view.DisplayAdminMenu(); // Calls the display.
            view.DisplayMessage(""); 

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (adminInput.ToLower())
                {

                    case "bands":
                        BandPanel();

                        invalid = false;

                    break;
                  
                    case "artists":
                        ArtistPanel();

                        invalid = false;

                    break;
                                        
                    case "albums":
                        AlbumPanel();

                        invalid = false;

                    break;

                    case "reviews":
                        ReviewPanel();

                        invalid = false;
                        
                    break;

                    case "genres":
                        GenrePanel();

                        invalid = false;

                    break;

                    case "formats":
                        FormatPanel();

                        invalid = false;

                    break;

                    case "rooms":
                        RoomPanel();

                        invalid = false;

                    break;

                    case "shelves":
                        ShelfPanel();

                        invalid = false;

                    break;

                    case "rows":
                        RowPanel();

                        invalid = false;

                    break;

                    case "tiers":
                        TierPanel();

                        invalid = false;

                    break;

                    case "accounts":
                        AccountPanel();

                        invalid = false;

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

        private static void GuestMenuscreenOptions() // The Guest Menuscreen. 
        {           
            bool invalid = true;

            string guestSelect = view.DisplayGuestMenu(); // Calls the display.
            view.DisplayMessage(""); 

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (guestSelect.ToLower())
                {

                    case "bands":
                        DefaultBandPanel();

                        invalid = false;

                    break;
                  
                    case "artists":
                        DefaultArtistPanel();

                        invalid = false;

                    break;

                    case "genres":
                        DefaultGenrePanel();

                        invalid = false;

                    break;

                    case "accounts":
                        DefaultAccountPanel();

                        invalid = false;

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

                        Thread.Sleep(wait);
                        Console.Clear();

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

                    /*
                    case 3:
                    // If the user is the very first person who installed the application, they get sent back to the 'true' admin menu.
                     
                        TrueAdminMenuscreenOptions();


                    break;
                    */

                 }


        }


        // The data-modifying commands for the Bands table.   

        private static void DefaultBandPanel()
        {
            string bandCmd;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            List<Bands> bands = storageManager.GetAllBands();
            view.DisplayBands(bands);

            bandCmd = view.DisplayEditingOptions("bands", "none");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (bandCmd.ToLower())
                {
                    case "back":
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        DefaultBandPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void BandPanel()
        {
            string bandCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            List<Bands> bands = storageManager.GetAllBands();
            view.DisplayBands(bands);

            bandCmd = view.DisplayEditingOptions("bands", "default~extras~search");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (bandCmd.ToLower())
                {
                    case "up":
                        UpdateBandName();

                        invalidCmd = false;

                        BandPanel();

                    break;

                    case "ins":
                        InsertNewBand();

                        invalidCmd = false;

                        BandPanel();

                    break;

                    case "del":
                        DeleteBandById();

                        invalidCmd = false;

                        BandPanel();

                    break;

                    case "search":
                        SearchBands();

                        invalidCmd = false;

                    break;

                    case "back":
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        BandPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateBandName()
        {

            try
            {

                List<Bands> bands = storageManager.GetAllBands();
                view.DisplayBands(bands);

                int bUpper = storageManager.GetBandBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int bandId = view.GetIntInput();
                bool bid = view.PassBoundary(bandId, bUpper);

                switch (bid)
                {
                    case true:
                        view.DisplayMessage("\nRename the record... ");
                        string bandName = view.GetInput();
                        bool bn = view.PassRange(bandName.Length, 3, 50);

                        switch (bn)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateBandById(bandId, bandName);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:
                                

                                BandPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        BandPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandPanel();
    
            }

        }  

        private static void InsertNewBand()
        {

            try
            {
                view.DisplayMessage("\nEnter the new band... ");
                string bandName = view.GetInput();
                bool bn = view.PassRange(bandName.Length, 3, 50);

                int bandId = 0;

                switch (bn)
                {
                    case true:
                        Bands newBand = new Bands(bandId, bandName);


                        int generatedId = storageManager.InsertBand(newBand);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        BandPanel();

                    break;
                }  

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandPanel();
    
            }

        }

        private static void DeleteBandById()
        {

            try
            {

                List<Bands> bands = storageManager.GetAllBands();
                view.DisplayBands(bands);

                int bUpper = storageManager.GetBandBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int bandId = view.GetIntInput();
                bool bid = view.PassBoundary(bandId, bUpper);
           
                int bandAlbumReference = storageManager.FetchBandAlbumBandReferences(bandId);

                switch (bid)
                {
                    case true:

                        if (bandId.Equals(bandAlbumReference))
                        {
                            view.DisplayReferentialError(wait);

                            BandPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteBandById(bandId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        BandPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandPanel();
    
            }

        }


        // The data-modifying commands for the Genres table.

        private static void DefaultGenrePanel()
        {
            string genreCmd;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            List<Genres> genres = storageManager.GetAllGenres();
            view.DisplayGenres(genres);

            genreCmd = view.DisplayEditingOptions("genres", "none");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (genreCmd.ToLower())
                {
                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        DefaultGenrePanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void GenrePanel()
        {
            string genreCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            List<Genres> genres = storageManager.GetAllGenres();
            view.DisplayGenres(genres);

            genreCmd = view.DisplayEditingOptions("genres", "default~extras~search");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (genreCmd.ToLower())
                {
                    case "up":
                        UpdateGenreName();

                        invalidCmd = false;

                        GenrePanel();

                    break;

                    case "ins":
                        InsertNewGenre();

                        invalidCmd = false;

                        GenrePanel();

                    break;

                    case "del":
                        DeleteGenreById();

                        invalidCmd = false;

                        GenrePanel();

                    break;

                    case "search":
                        SearchGenres();


                        invalidCmd = false;

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        GenrePanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateGenreName()
        {

            try
            {

                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int gUpper = storageManager.GetGenreBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int genreId = view.GetIntInput();
                bool gid = view.PassBoundary(genreId, gUpper);

                switch (gid)
                {
                    case true:
                        view.DisplayMessage("\nRename the record... ");
                        string genreName = view.GetInput();
                        bool gn = view.PassRange(genreName.Length, 3, 20);

                        switch (gn)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateGenreById(genreId, genreName);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:
                                

                                GenrePanel();

                            break;
                        }

                    break;

                    case false:
                        

                        GenrePanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                GenrePanel();
    
            }

        }  

        private static void InsertNewGenre()
        {

            try
            {
                view.DisplayMessage("\nEnter the new genre... ");
                string genreName = view.GetInput();
                bool gn = view.PassRange(genreName.Length, 3, 20);

                int genreId = 0;

                switch (gn)
                {
                    case true:
                        Genres newGenre = new Genres(genreId, genreName);


                        int generatedId = storageManager.InsertGenre(newGenre);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        GenrePanel();

                    break;
                }  

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                GenrePanel();
    
            }

        }

        private static void DeleteGenreById()
        {

            try
            {

                List<Genres> genres = storageManager.GetAllGenres();
                view.DisplayGenres(genres);

                int gUpper = storageManager.GetGenreBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int genreId = view.GetIntInput();
                bool gid = view.PassBoundary(genreId, gUpper);
           
                int genreAAlbumReference = storageManager.FetchArtistAlbumGenreReferences(genreId);
                int genreBAlbumReference = storageManager.FetchBandAlbumGenreReferences(genreId);

                switch (gid)
                {
                    case true:

                        if (genreId.Equals(genreAAlbumReference) | genreId.Equals(genreBAlbumReference))
                        {
                            view.DisplayReferentialError(wait);

                            GenrePanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteGenreById(genreId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        GenrePanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                GenrePanel();
    
            }

        }


        // The data-modifying commands for the Formats table.

        private static void FormatPanel()
        {
            string formatCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            List<Formats> formats = storageManager.GetAllFormats();

            formatCmd = view.DisplayEditingOptions("formats", "default");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (formatCmd.ToLower())
                {
                    case "up":
                        UpdateFormatName();

                        invalidCmd = false;

                        FormatPanel();

                    break;

                    case "ins":
                        InsertNewFormat();

                        invalidCmd = false;

                        FormatPanel();

                    break;

                    case "del":
                        DeleteFormatById();

                        invalidCmd = false;

                        FormatPanel();

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        FormatPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateFormatName()
        {

            try
            {

                List<Formats> formats = storageManager.GetAllFormats();
                int fUpper = storageManager.GetFormatBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int formatId = view.GetIntInput();
                bool fid = view.PassBoundary(formatId, fUpper);

                switch (fid)
                {
                    case true:
                        view.DisplayMessage("\nRename the record... ");
                        string formatName = view.GetInput();
                        bool fn = view.PassRange(formatName.Length, 2, 15);

                        switch (fn)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateFormatById(formatId, formatName);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:
                                

                                FormatPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        FormatPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                FormatPanel();
    
            }

        }  

        private static void InsertNewFormat()
        {

            try
            {
                view.DisplayMessage("\nEnter the new format... ");
                string formatName = view.GetInput();
                bool fn = view.PassRange(formatName.Length, 2, 15);

                int formatId = 0;

                switch (fn)
                {
                    case true:
                        Formats newFormat = new Formats(formatId, formatName);


                        int generatedId = storageManager.InsertFormat(newFormat);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        FormatPanel();

                    break;
                }  

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                FormatPanel();
    
            }

        }

        private static void DeleteFormatById()
        {

            try
            {

                List<Formats> formats = storageManager.GetAllFormats();
                int fUpper = storageManager.GetFormatBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int formatId = view.GetIntInput();
                bool fid = view.PassBoundary(formatId, fUpper);
           
                int formatAAlbumReference = storageManager.FetchArtistAlbumFormatReferences(formatId);
                int formatBAlbumReference = storageManager.FetchBandAlbumFormatReferences(formatId);

                switch (fid)
                {
                    case true:

                        if (formatId.Equals(formatAAlbumReference) | formatId.Equals(formatBAlbumReference))
                        {
                            view.DisplayReferentialError(wait);

                            FormatPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteFormatById(formatId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        FormatPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                FormatPanel();
    
            }

        }


        // The data-modifying commands for the Artists table.

        private static void DefaultArtistPanel()
        {
            string artistCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            List<Artists> artists = storageManager.GetAllArtists();
            view.DisplayArtists(artists);

            artistCmd = view.DisplayEditingOptions("artists", "none");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (artistCmd.ToLower())
                {
                    case "back":
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        DefaultArtistPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void ArtistPanel()
        {
            string artistCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            List<Artists> artists = storageManager.GetAllArtists();
            view.DisplayArtists(artists);

            artistCmd = view.DisplayEditingOptions("artists", "default~extras~search");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (artistCmd.ToLower())
                {
                    case "up":
                        UpdateArtistName();

                        invalidCmd = false;

                        ArtistPanel();

                    break;

                    case "ins":
                        InsertNewArtist();

                        invalidCmd = false;

                        ArtistPanel();

                    break;

                    case "del":
                        DeleteArtistById();

                        invalidCmd = false;

                        ArtistPanel();

                    break;

                    case "search":
                        SearchArtists();

                        invalidCmd = false;

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        ArtistPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateArtistName()
        {

            try
            {

                List<Artists> artists = storageManager.GetAllArtists();
                view.DisplayArtists(artists);

                int aUpper = storageManager.GetArtistBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int artistId = view.GetIntInput();
                bool aid = view.PassBoundary(artistId, aUpper);

                switch (aid)
                {
                    case true:
                        view.DisplayMessage("\nRename the record... ");
                        string artistName = view.GetInput();
                        bool an = view.PassRange(artistName.Length, 3, 50);

                        switch (an)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateArtistById(artistId, artistName);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:


                                ArtistPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        ArtistPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistPanel();
    
            }

        }  

        private static void InsertNewArtist()
        {

            try
            {
                view.DisplayMessage("\nEnter the new artist... ");
                string artistName = view.GetInput();
                bool an = view.PassRange(artistName.Length, 3, 50);

                int artistId = 0;

                switch (an)
                {
                    case true:
                        Artists newArtist = new Artists(artistId, artistName);


                        int generatedId = storageManager.InsertArtist(newArtist);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        ArtistPanel();

                    break;
                }  

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistPanel();
    
            }

        }

        private static void DeleteArtistById()
        {

            try
            {

                List<Artists> artists = storageManager.GetAllArtists();
                view.DisplayArtists(artists);

                int aUpper = storageManager.GetArtistBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int artistId = view.GetIntInput();
                bool aid = view.PassBoundary(artistId, aUpper);
           
                int artistAlbumReference = storageManager.FetchArtistAlbumArtistReferences(artistId);

                switch (aid)
                {
                    case true:

                        if (artistId.Equals(artistAlbumReference))
                        {
                            view.DisplayReferentialError(wait);

                            ArtistPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteArtistById(artistId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        ArtistPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistPanel();
    
            }

        }


        // The data-modifying commands for the Rooms table.

        private static void RoomPanel()
        {
            string roomCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllRooms();

            roomCmd = view.DisplayEditingOptions("rooms", "default");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (roomCmd.ToLower())
                {
                    case "up":
                        UpdateRoomName();

                        invalidCmd = false;

                        RoomPanel();

                    break;

                    case "ins":
                        InsertNewRoom();

                        invalidCmd = false;

                        RoomPanel();

                    break;

                    case "del":
                        DeleteRoomById();

                        invalidCmd = false;

                        RoomPanel();

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        RoomPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateRoomName()
        {

            try
            {

                List<Rooms> rooms = storageManager.GetAllRooms();
                int rUpper = storageManager.GetRoomBoundary();


                view.DisplayMessage("\nEnter the identification number... ");
                int roomId = view.GetIntInput();
                bool rid = view.PassBoundary(roomId, rUpper);

                switch (rid)
                {
                    case true:
                        view.DisplayMessage("\nRename the record... ");
                        string roomName = view.GetInput();
                        bool rn = view.PassRange(roomName.Length, 3, 20);

                        switch (rn)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateRoomById(roomId, roomName);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:


                                RoomPanel();

                            break;
                        }

                    break;

                    case false:
                        

                       RoomPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RoomPanel();
    
            }

        }  

        private static void InsertNewRoom()
        {

            try
            {
                view.DisplayMessage("\nEnter the new room... ");
                string roomName = view.GetInput();
                bool rn = view.PassRange(roomName.Length, 3, 20);

                int roomId = 0;

                switch (rn)
                {
                    case true:
                        Rooms newRoom = new Rooms(roomId, roomName);


                        int generatedId = storageManager.InsertRoom(newRoom);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        RoomPanel();

                    break;
                }  

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RoomPanel();
    
            }

        }

        private static void DeleteRoomById()
        {

            try
            {

                List<Rooms> rooms = storageManager.GetAllRooms();
                int rUpper = storageManager.GetRoomBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int roomId = view.GetIntInput();
                bool rid = view.PassBoundary(roomId, rUpper);
           
                int roomShelfReference = storageManager.FetchShelfRoomReferences(roomId);

                switch (rid)
                {
                    case true:

                        if (roomId.Equals(roomShelfReference))
                        {
                            view.DisplayReferentialError(wait);

                            RoomPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteRoomById(roomId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        RoomPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RoomPanel();
    
            }

        }

        // The Shelves.

        private static void ShelfReportPanel()
        {
            int shelfReportSelect;
            string shelfCmd;
            bool invalidCmd;

            try
            {
                shelfReportSelect = view.DisplayReportOptions("shelves", "rows~shelves");

                Thread.Sleep(wait);
                Console.Clear();

                do
                {
                    switch (shelfReportSelect)
                    {
                        case 1:
                            storageManager.GetTotalOfRowOccupancyPerShelf();

                            shelfCmd = view.GetInput();

                            do
                            {
                                switch (shelfCmd.ToUpper())
                                {
                                    case "E":
                                        ShelfReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ShelfReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
    

                        break;

                        case 2:                                   
                            ShelfPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            ShelfReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }
            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                ShelfReportPanel();

            }

        }

        private static void RowReportPanel()
        {
            int rowReportSelect;
            string rowCmd;
            bool invalidCmd;

            try
            {
                rowReportSelect = view.DisplayReportOptions("rows", "rows~shelves");

                Thread.Sleep(wait);
                Console.Clear();

                invalidCmd = false;

                do
                {
                    switch (rowReportSelect)
                    {
                        case 1:
                            storageManager.GetTotalOfRowOccupancyPerShelf();

                            rowCmd = view.GetInput();

                            do
                            {
                                switch (rowCmd.ToUpper())
                                {
                                    case "E":
                                        RowReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        RowReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
    

                        break;

                        case 2:                                   
                            RowPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            RowReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }
            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                RowReportPanel();

            }

        }

        // The data-modifying commands for the Shelves table.

        private static void ShelfPanel()
        {
            string shelfCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllShelves();

            shelfCmd = view.DisplayEditingOptions("shelves", "default~extras~view");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (shelfCmd.ToLower())
                {
                    case "up":
                        UpdateShelfRoom();

                        invalidCmd = false;

                        ShelfPanel();

                    break;

                    case "ins":
                        InsertNewShelf();

                        invalidCmd = false;

                        ShelfPanel();

                    break;

                    case "del":
                        DeleteShelfById();

                        invalidCmd = false;

                        ShelfPanel();

                    break;

                    case "reports":

                        ShelfReportPanel();

                        invalidCmd = false;
            

                    break;
        
                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        ShelfPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateShelfRoom()
        {
            

            try
            {

                List<Shelves> shelves = storageManager.GetAllShelves();
                int sUpper = storageManager.GetShelfBoundary();

                view.DisplayMessage("\nEnter the shelf identification number... ");
                int shelfTagId = view.GetIntInput();
                bool staid = view.PassBoundary(shelfTagId, sUpper);

                switch (staid)
                {
                    case true:

                        List<Rooms> rooms = storageManager.GetAllRooms();
                        int rUpper = storageManager.GetRoomBoundary();

                        view.DisplayMessage("\nEnter the new room identification number... ");
                        int roomId = view.GetIntInput();
                        bool rid = view.PassBoundary(roomId, rUpper);

                        switch (rid)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateShelfRoomById(shelfTagId, roomId);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:


                                ShelfPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        ShelfPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ShelfPanel();
    
            }

        }
        
        private static void InsertNewShelf()
        {
            

            try
            {

                List<Shelves> shelves = storageManager.GetAllShelves();
                view.DisplayMessage("\nEnter the new shelves tag... ");
                char shelfTag = view.GetCharInput();

                view.DisplayMessage("\nEnter the new room... ");
                List<Rooms> rooms = storageManager.GetAllRooms();

                int rUpper = storageManager.GetRoomBoundary();

                int roomId = view.GetIntInput();
                bool rid = view.PassBoundary(roomId, rUpper);
                int shelfTagId = 0;

                switch (rid)
                {
                    case true:
                        Shelves newShelf = new Shelves(shelfTagId, shelfTag, roomId);

                        int generatedId = storageManager.InsertShelf(newShelf);
                        view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                    break;

                    case false:
                        

                        ShelfPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ShelfPanel();

            }

        }

        private static void DeleteShelfById()
        {

            try
            {

                List<Shelves> shelves = storageManager.GetAllShelves();
                int sUpper = storageManager.GetShelfBoundary();

                Console.WriteLine(sUpper);

                view.DisplayMessage("\nEnter the identification number... ");
                int shelfTagId = view.GetIntInput();
                bool staid = view.PassBoundary(shelfTagId, sUpper);
           
                int shelfRowReference = storageManager.FetchRowShelfReferences(shelfTagId);

                switch (staid)
                {
                    case true:

                        if (shelfTagId.Equals(shelfRowReference))
                        {
                            view.DisplayReferentialError(wait);

                            ShelfPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteShelfById(shelfTagId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        ShelfPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ShelfPanel();
            }

        }


        // The data-modifying commands for the Rows table.

        private static void RowPanel()
        {
            string rowCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllRows();

            rowCmd = view.DisplayEditingOptions("rows", "default~extras~view");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (rowCmd.ToLower())
                {
                    case "up":
                        UpdateRow();

                        invalidCmd = false;

                        RowPanel();

                    break;

                    case "ins":
                        InsertNewRow();

                        invalidCmd = false;

                        RowPanel();

                    break;

                    case "del":
                        DeleteRowById();

                        invalidCmd = false;

                        RowPanel();

                    break;

                    case "reports":
                        RowReportPanel();

                        invalidCmd = false;

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        RowPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);


        }

        private static void UpdateRow()
        {
            

            try
            {

                List<Rows> rows = storageManager.GetAllRows();
                int rUpper = storageManager.GetRowBoundary();

                view.DisplayMessage("\nEnter the row identification number... ");
                int shelfRowId = view.GetIntInput();
                bool sroid = view.PassBoundary(shelfRowId, rUpper);

                switch (sroid)
                {
                    case true:

                        List<Shelves> shelves = storageManager.GetAllShelves();
                        int shelvesUpper = storageManager.GetShelfBoundary();

                        view.DisplayMessage("\nEnter the new shelf identification number... ");
                        int shelfTagId = view.GetIntInput();
                        bool staid = view.PassBoundary(shelfTagId, shelvesUpper);

                        switch (staid)
                        {
                            case true:
                                int rowsAffected = storageManager.UpdateRowsShelfById(shelfRowId, shelfTagId);
                                view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                            break;

                            case false:


                                RowPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        RowPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RowPanel();
    
            }

        }

        private static void InsertNewRow()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new rows number... ");
                int shelfRow = view.GetIntInput();
                bool sr = view.PassRange(shelfRow, 1, 6);

                switch (sr)
                {
                    case true:
                        view.DisplayMessage("\nEnter the shelf it belongs on... ");
                        List<Shelves> shelves = storageManager.GetAllShelves();

                        int sUpper = storageManager.GetShelfBoundary();

                        int shelfTagId = view.GetIntInput();
                        bool staid = view.PassBoundary(shelfTagId, sUpper);
                        int shelfRowId = 0;

                        switch (staid)
                        {
                            case true:
                                Rows newRow = new Rows(shelfRowId, shelfRow, shelfTagId);

                                int generatedId = storageManager.InsertRow(newRow);
                                view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                            break;

                            case false:
        

                                RowPanel();

                            break;
                        }

                    break;

                    case false:


                        RowPanel();

                    break;
                }

                

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RowPanel();

            }

        }

        private static void DeleteRowById()
        {

            try
            {

                List<Rows> rows = storageManager.GetAllRows();
                int rUpper = storageManager.GetRowBoundary();

                Console.WriteLine(rUpper);

                view.DisplayMessage("\nEnter the identification number... ");
                int rowId = view.GetIntInput();
                bool sroid = view.PassBoundary(rowId, rUpper);
           
                int rowAAlbumReference = storageManager.FetchArtistAlbumRowReferences(rowId);
                int rowBAlbumReference = storageManager.FetchBandAlbumRowReferences(rowId);

                switch (sroid)
                {
                    case true:

                        if (rowId.Equals(rowAAlbumReference) | rowId.Equals(rowBAlbumReference))
                        {
                            view.DisplayReferentialError(wait);

                            RowPanel();

                        }

                        else 
                        { 
                            int rowsAffected = storageManager.DeleteRowById(rowId);
                            view.DisplayMessage($"\nDeleted {rowsAffected} row.");


                        }
                        

                    break;

                    case false:
                        

                        RowPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                RowPanel();
    
            }

        }


        // The data-modifying commands for the accounts.

        private static void DefaultAccountPanel()
        {
            string accountSelect;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            accountSelect = view.DisplayEditingOptions("accounts", "df~account~variants");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (accountSelect)
                {
                    case "default":

                        logStatus = true;
                        CreateUser();

                        invalidCmd = false;

                    break;

                    case "back":
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        DefaultAccountPanel();

                        invalidCmd = true;

                    break;

                }

            } while (invalidCmd);

        }

        private static void AccountPanel()
        {
            string accountCmd;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            accountCmd = view.DisplayEditingOptions("accounts", "account~variants");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (accountCmd.ToLower())
                {
                    case "default":
                        logStatus = true;
                        CreateUser();

                        invalidCmd = false;

                    break;

                    case "admin":
                        CreateAdmin();

                        invalidCmd = false;

                    break;

                    case "back":
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        AccountPanel();

                        invalidCmd = true;

                    break;

                }

            } while (invalidCmd);

        }

        private static void CreateUser()
        {
            
            
            try
            {
                

                view.DisplayMessage("[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

                view.DisplayMessage("\nEnter your first name... ");
                view.DisplayMessage(" ");
                string fName = view.GetInput();
                bool fname = view.PassRange(fName.Length, 3, 20);

                int personId = 0;

                switch (fname)
                {
                    case true:
                        view.DisplayMessage("\nEnter your last name... ");
                        view.DisplayMessage(" ");
                        string sName = view.GetInput();
                        bool sname = view.PassRange(sName.Length, 3, 20);

                        switch (sname)
                        {
                            case true:
                                view.DisplayMessage("\nCreate a username... ");
                                view.DisplayMessage(" ");
                                string newuser = view.GetInput();
                                bool username = view.PassRange(newuser.Length, 3, 15);

                                switch (username)
                                {
                                    case true:
                                        int rid = 1;

                                        view.DisplayMessage("\nCreate a password... ");
                                        view.DisplayMessage(" ");
                                        string newpw = view.GetInput();
                                        bool password = view.PassRange(newpw.Length, 3, 30);

                                        switch (password)
                                        {
                                            case true:
                                                if (newuser.Equals(storageManager.FetchUsername(newuser)))
                                                {
                                                    view.DisplayMessage("\nThat username is already taken. Choose a different username. ");
                                                    Thread.Sleep(wait);
                                                    Console.Clear();

                                                    switch (logStatus)
                                                    {
                                                        case true:
                                                            switch (roleId)
                                                            {
                                                                case 1:
                                                                    DefaultAccountPanel();

                                                                break;

                                                                case 2:
                                                                    AccountPanel();

                                                                break;
                                                            }

                                                        break;

                                                        case false:
                                                            StartMenuscreenOptions();

                                                        break;
                                                    }

                                                }

                                                else
                                                {
                                                    Accounts newUser = new Accounts(personId, fName, sName, newuser, newpw, rid);


                                                    int generatedId = storageManager.CreateAccount(newUser);

                                                    view.DisplayMessage("\n  Successful. Booting you to the login/register.");

                                                    // Resets the accounts credentials.

                                                    roleId = 0;
                                                    accountId = 0;
                                                    storageManager.CloseConnection(); // Closes the connection upon signing out.

                                                    Thread.Sleep(wait);
                                                    Console.Clear();
                                                    StartMenuscreenOptions();

                                                }

                                            break;

                                            case false:
                                                
                                                switch (logStatus)
                                                {
                                                    case true:
                                                        switch (roleId)
                                                        {
                                                            case 1:
                                                                DefaultAccountPanel();

                                                            break;

                                                            case 2:
                                                                AccountPanel();

                                                            break;
                                                        }

                                                    break;

                                                    case false:
                                                        StartMenuscreenOptions();

                                                    break;
                                                }
                                                

                                            break;
                                        }

                                        break;

                                    case false:

                                        switch (logStatus)
                                        {
                                            case true:
                                                switch (roleId)
                                                {
                                                    case 1:
                                                        DefaultAccountPanel();

                                                    break;

                                                    case 2:
                                                        AccountPanel();

                                                    break;
                                                }

                                            break;

                                            case false:
                                                StartMenuscreenOptions();

                                            break;
                                        }
                                        

                                    break;

                                }

                                break;

                            case false:
                                

                                switch (logStatus)
                                {
                                    case true:
                                        switch (roleId)
                                        {
                                            case 1:
                                                DefaultAccountPanel();

                                            break;

                                            case 2:
                                                AccountPanel();

                                            break;
                                        }

                                    break;

                                    case false:
                                        StartMenuscreenOptions();

                                    break;
                                }

                            break;

                        }

                        break;

                    case false:
                        

                        switch (logStatus)
                        {
                            case true:
                                switch (roleId)
                                {
                                    case 1:
                                        DefaultAccountPanel();

                                    break;

                                    case 2:
                                        AccountPanel();

                                    break;
                                }

                            break;

                            case false:
                                StartMenuscreenOptions();

                            break;
                        }

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                switch (logStatus)
                {
                    case true:
                        switch (roleId)
                        {
                            case 1:
                                DefaultAccountPanel();

                            break;

                            case 2:
                                AccountPanel();

                            break;
                        }

                    break;

                    case false:
                        StartMenuscreenOptions();

                    break;
                }

            }

        }

        private static void CreateAdmin()
        {
            
            
            try
            {
                

                view.DisplayMessage("[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

                view.DisplayMessage("\nEnter your first name... ");
                view.DisplayMessage(" ");
                string fName = view.GetInput();
                bool fname = view.PassRange(fName.Length, 3, 20);

                int personId = 0;

                switch (fname)
                {
                    case true:
                        view.DisplayMessage("\nEnter your last name... ");
                        view.DisplayMessage(" ");
                        string sName = view.GetInput();
                        bool sname = view.PassRange(sName.Length, 3, 20);

                        switch (sname)
                        {
                            case true:
                                view.DisplayMessage("\nCreate a username... ");
                                view.DisplayMessage(" ");
                                string newuser = view.GetInput();
                                bool username = view.PassRange(newuser.Length, 3, 15);

                                switch (username)
                                {
                                    case true:
                                        int rid = 2;

                                        view.DisplayMessage("\nCreate a password... ");
                                        view.DisplayMessage(" ");
                                        string newpw = view.GetInput();
                                        bool password = view.PassRange(newpw.Length, 3, 30);

                                        switch (password)
                                        {
                                            case true:
                                                if (newuser.Equals(storageManager.FetchUsername(newuser)))
                                                {
                                                    view.DisplayMessage("\nThat username is already taken. Choose a different username. ");
                                                    Thread.Sleep(wait);
                                                    Console.Clear();
                                                    
                                                    AccountPanel();
                                                               
                                                }

                                                else
                                                {
                                                    Accounts newAdmin = new Accounts(personId, fName, sName, newuser, newpw, rid);


                                                    int generatedId = storageManager.CreateAccount(newAdmin);

                                                    view.DisplayMessage("\n  Successful. Booting you to the login/register.");

                                                    // Resets the accounts credentials.

                                                    roleId = 0;
                                                    accountId = 0;
                                                    storageManager.CloseConnection(); // Closes the connection upon signing out.

                                                    Thread.Sleep(wait);
                                                    Console.Clear();
                                                    StartMenuscreenOptions();

                                                }

                                            break;

                                            case false:
                                                

                                                AccountPanel();

                                            break;
                                        }

                                        break;

                                    case false:
                                        

                                        AccountPanel();

                                    break;

                                }

                                break;

                            case false:
                                

                                AccountPanel();

                            break;

                        }

                        break;

                    case false:
                        

                        AccountPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                AccountPanel();

            }

        }


        // The Albums.

        private static void AlbumPanel()
        {

            string albumSelect;
            bool invalidAlbumSelect;

            Thread.Sleep(wait);
            Console.Clear();

            albumSelect = view.DisplayEditingOptions("albums", "album~variants");

            Thread.Sleep(wait);
            Console.Clear();


            do
            {
                switch (albumSelect)
                {
                    case "artists":
                        ArtistAlbumPanel();

                        invalidAlbumSelect = false;

                    break;

                    case "bands":
                        BandAlbumPanel();

                        invalidAlbumSelect = false;

                    break;

                    case "back":
                        GoBack();

                        invalidAlbumSelect = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        AlbumPanel();

                        invalidAlbumSelect = true;

                    break;

                }

            } while (invalidAlbumSelect);

        }

        private static void ArtistAlbumReportPanel()
        {
            string aAlbumCmd;
            int aAlbumReportSelect;
            bool invalidCmd;

            try
            {
                aAlbumReportSelect = view.DisplayReportOptions("artists", "artists");

                Thread.Sleep(wait);
                Console.Clear();

                invalidCmd = false;

                do
                {
                    switch (aAlbumReportSelect)
                    {
                        case 1:
                            storageManager.GetArtistReleaseDate();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 2:
                            storageManager.GetAToJArtists();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 3:
                            storageManager.GetArtistsEarly2000sMusic();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 4:
                            storageManager.GetTotalArtistGenres();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 5:
                            storageManager.GetArtistsAsAWhole();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 6:
                            storageManager.GetTotalAlbumsPublishedByArtists();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 7:
                            storageManager.GetTotalPublishingsOfAllArtistsPerYear();

                            aAlbumCmd = view.GetInput();

                            do
                            {
                                switch (aAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 8:                                   
                            ArtistAlbumPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            ArtistAlbumReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }

            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                ArtistAlbumReportPanel();

            }

        }

        private static void BandAlbumReportPanel()
        {
            string bAlbumCmd;
            int bAlbumReportSelect;
            bool invalidCmd;

            try
            {
                bAlbumReportSelect = view.DisplayReportOptions("bands", "bands");

                Thread.Sleep(wait);
                Console.Clear();

                invalidCmd = false;

                do
                {
                    switch (bAlbumReportSelect)
                    {
                        case 1:
                            storageManager.GetBandReleaseDate();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 2:
                            storageManager.GetAToJBands();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 3:
                            storageManager.GetBandsEarly2000sMusic();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 4:
                            storageManager.GetTotalBandGenres();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 5:
                            storageManager.GetBandsAsAWhole();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 6:
                            storageManager.GetTotalAlbumsPublishedByBands();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 7:
                            storageManager.GetTotalPublishingsOfAllBandsPerYear();

                            bAlbumCmd = view.GetInput();

                            do
                            {
                                switch (bAlbumCmd.ToUpper())
                                {
                                    case "E":
                                        BandAlbumReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandAlbumReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);
            

                        break;

                        case 8:                                   
                            BandAlbumPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            BandAlbumReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }

            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                BandAlbumReportPanel();

            }

        }


        // The data-modifying commands for the Artist Albums table.

        private static void ArtistAlbumPanel()
        {
            
            string aAlbumCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllArtistAlbums();

            aAlbumCmd = view.DisplayEditingOptions("artist-albums", "album~extras");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (aAlbumCmd.ToLower())
                {
                    case "up":
                        UpdateArtistAlbum();

                        invalidCmd = false;

                        ArtistAlbumPanel();

                    break;

                    case "ins":
                        InsertArtistAlbum();

                        invalidCmd = false;

                        ArtistAlbumPanel();

                    break;

                    case "del":
                        DeleteArtistAlbumById();

                        invalidCmd = false;

                        ArtistAlbumPanel();

                    break;

                    case "reports":
                        ArtistAlbumReportPanel();

                        invalidCmd = false;

                    break;

                    case "search":
                        SearchArtistAlbums();
                        
                        invalidCmd = false;

                    break;

                    case "lost":
                        MarkArtistAsLost();

                        invalidCmd = false;

                        ArtistAlbumPanel();

                    break;

                    case "back":
                        AlbumPanel();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        ArtistAlbumPanel();

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateArtistAlbum()
        {


            try
            {
                view.DisplayMessage("\nEnter an album identification number... ");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();
                int alUpper = storageManager.GetArtistAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter the new album name... ");

                        string albumName = view.GetInput();
                        bool aln = view.PassRange(albumName.Length, 3, 50);

                        switch (aln)
                        {
                            case true:
                                view.DisplayMessage("\nEnter a new genre identification number...");
                                List<Genres> genres = storageManager.GetAllGenres();
                                int gUpper = storageManager.GetGenreBoundary();

                                view.DisplayGenres(genres);

                                int genreId = view.GetIntInput();
                                bool gid = view.PassBoundary(genreId, gUpper);

                                switch (gid)
                                {
                                    case true:
                                        view.DisplayMessage("\nEnter a new date of release... YYYY/MM/DD");
                                        DateTime dateOfRelease = view.GetDateTimeInput();
                                        bool dtor = view.PassDateBoundary(dateOfRelease);

                                        switch (dtor)
                                        {
                                            case true:
                                                view.DisplayMessage("\nEnter a new format identification number... ");
                                                List<Formats> formats = storageManager.GetAllFormats();
                                                int fUpper = storageManager.GetFormatBoundary();

                                                int formatId = view.GetIntInput();
                                                bool fid = view.PassBoundary(formatId, fUpper);

                                                switch (fid)
                                                {
                                                    case true:
                                                        view.DisplayMessage("\nEnter a new artist identification number... ");
                                                        List<Artists> artists = storageManager.GetAllArtists();
                                                        int aUpper = storageManager.GetArtistBoundary();

                                                        view.DisplayArtists(artists);

                                                        int artistId = view.GetIntInput();
                                                        bool aid = view.PassBoundary(artistId, aUpper);

                                                        switch (aid)
                                                        {
                                                            case true:
                                                                view.DisplayMessage("\nEnter a new shelf row identification number... ");
                                                                List<Rows> rows = storageManager.GetAllRows();
                                                                int rUpper = storageManager.GetRowBoundary();

                                                                int shelfRowId = view.GetIntInput();
                                                                bool sroid = view.PassBoundary(shelfRowId, rUpper);



                                                                switch (sroid)
                                                                {
                                                                    case true:
                                                                        int rowsAffected = storageManager.UpdateArtistAlbumById(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId);
                                                                        view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                                                                    break;

                                                                    case false:
                                                                        

                                                                        ArtistAlbumPanel();

                                                                    break;

                                                                }

                                                            break;

                                                            case false:
                                                                

                                                                ArtistAlbumPanel();

                                                            break;
                                                        }

                                                        break;

                                                    case false:
                                                        

                                                        ArtistAlbumPanel();

                                                    break;

                                                }

                                            break;

                                            case false:
                                                

                                                ArtistAlbumPanel();

                                            break;

                                        }

                                        

                                    break;

                                    case false:
                                        

                                        ArtistAlbumPanel();

                                    break;
                                }

                            break;

                            case false:
                                

                                ArtistAlbumPanel();

                            break;
                            
                        }

                    break;

                    case false:
                        

                        ArtistAlbumPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistAlbumPanel();

            }

        }
        
        private static void InsertArtistAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new album... ");
                string albumName = view.GetInput();
                bool aln = view.PassRange(albumName.Length, 3, 50);

                int albumId = 0;

                switch (aln)
                {
                    case true:
                        view.DisplayMessage("\nEnter a genre identification number...");
                        List<Genres> genres = storageManager.GetAllGenres();
                        int gUpper = storageManager.GetGenreBoundary();

                        view.DisplayGenres(genres);

                        int genreId = view.GetIntInput();
                        bool gid = view.PassBoundary(genreId, gUpper);

                        switch (gid)
                        {
                            case true:
                                view.DisplayMessage("\nEnter the date of release... YYYY/MM/DD");
                                DateTime dateOfRelease = view.GetDateTimeInput();
                                bool dtor = view.PassDateBoundary(dateOfRelease);

                                switch (dtor)
                                {
                                    case true:
                                        view.DisplayMessage("\nEnter the format identification number... ");
                                        List<Formats> formats = storageManager.GetAllFormats();
                                        int fUpper = storageManager.GetFormatBoundary();

                                        int formatId = view.GetIntInput();
                                        bool fid = view.PassBoundary(formatId, fUpper);

                                        switch (fid)
                                        {
                                            case true:
                                                view.DisplayMessage("\nEnter the artist identification number... ");
                                                List<Artists> artists = storageManager.GetAllArtists();
                                                int aUpper = storageManager.GetArtistBoundary();

                                                view.DisplayArtists(artists);

                                                int artistId = view.GetIntInput();
                                                bool aid = view.PassBoundary(artistId, aUpper);

                                                switch (aid)
                                                {
                                                    case true:
                                                        List<Rows> rows = storageManager.GetAllRows();
                                                        int rUpper = storageManager.GetRowBoundary();

                                                        int shelfRowId = view.GetIntInput();
                                                        bool sroid = view.PassBoundary(shelfRowId, rUpper);

                                                        bool lost = false;

                                                        switch (sroid)
                                                        {
                                                            case true:
                                                                ArtistAlbums newAlbum = new ArtistAlbums(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId, lost);

                                                                int generatedId = storageManager.InsertArtistAlbum(newAlbum);
                                                                view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                                                            break;

                                                            case false:


                                                                ArtistAlbumPanel();

                                                            break;
                                                        }

                                                    break;

                                                    case false:
                                                        

                                                        ArtistAlbumPanel();

                                                    break;
                                                }

                                                break;

                                            case false:
                                                

                                                ArtistAlbumPanel();

                                            break;
                                        }

                                        break;

                                    case false:
                                        

                                        ArtistAlbumPanel();

                                    break;
                                }

                            break;

                            case false:
                                

                                ArtistAlbumPanel();

                            break;
                        }


                    break;

                    case false:
                        

                        ArtistAlbumPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistAlbumPanel();

            }
            
        }
        
        private static void DeleteArtistAlbumById()
        {

            try
            {

                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();
                int alUpper = storageManager.GetArtistAlbumBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);
           
                int AAlbumAReviewReference = storageManager.FetchArtistReviewArtistAlbumReferences(albumId);

                switch (alid)
                {
                    case true:

                        if (albumId.Equals(AAlbumAReviewReference))
                        {
                            view.DisplayReferentialError(wait);

                            ArtistAlbumPanel();

                        }

                        else 
                        { 
                            int albumssAffected = storageManager.DeleteArtistAlbumById(albumId);
                            view.DisplayMessage($"\nDeleted {albumssAffected} albums.");


                        }
                        

                    break;

                    case false:
                        

                        ArtistAlbumPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistAlbumPanel();
    
            }

        }

        private static void MarkArtistAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<ArtistAlbums> artists = storageManager.GetAllArtistAlbums();
                int alUpper = storageManager.GetArtistAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                bool lost = true;

                    switch (storageManager.FetchLostFromArtistAlbums(albumId))
                    {
                        case true:
                            lost = false;

                        break;

                        case false:
                            lost = true;

                        break;
                    }

                switch (alid)
                {
                    case true:
                        int rowsAffected = storageManager.LostArtist(albumId, lost);
                        view.DisplayMessage($"\nMarked/unmarked {rowsAffected} records as lost.");

                    break;

                    case false:
                        ArtistAlbumPanel();

                    break;
                }

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistAlbumPanel();

            }

        }


        // The data-modifying commands for the Band Albums table.

        private static void BandAlbumPanel()
        {
            
            string bAlbumCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllBandAlbums();

            bAlbumCmd = view.DisplayEditingOptions("band-albums", "album~extras");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (bAlbumCmd.ToLower())
                {
                    case "up":
                        UpdateBandAlbum();

                        invalidCmd = false;

                        BandAlbumPanel();

                    break;

                    case "ins":
                        InsertBandAlbum();

                        invalidCmd = false;

                        BandAlbumPanel();

                    break;

                    case "del":
                        DeleteBandAlbumById();

                        invalidCmd = false;

                        BandAlbumPanel();

                    break;

                    case "reports":
                        BandAlbumReportPanel();

                        invalidCmd = false;

                    break;

                    case "search":
                        SearchBandAlbums();
                        
                        invalidCmd = false;

                    break;

                    case "lost":
                        MarkBandAsLost();

                        invalidCmd = false;

                        BandAlbumPanel();

                    break;

                    case "back":
                        AlbumPanel();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        BandAlbumPanel();

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateBandAlbum()
        {


            try
            {
                view.DisplayMessage("\nEnter an album identification number... ");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();
                int alUpper = storageManager.GetBandAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter the new album name... ");

                        string albumName = view.GetInput();
                        bool aln = view.PassRange(albumName.Length, 3, 50);

                        switch (aln)
                        {
                            case true:
                                view.DisplayMessage("\nEnter a new genre identification number...");
                                List<Genres> genres = storageManager.GetAllGenres();
                                int gUpper = storageManager.GetGenreBoundary();

                                view.DisplayGenres(genres);

                                int genreId = view.GetIntInput();
                                bool gid = view.PassBoundary(genreId, gUpper);

                                switch (gid)
                                {
                                    case true:
                                        view.DisplayMessage("\nEnter a new date of release... YYYY/MM/DD");
                                        DateTime dateOfRelease = view.GetDateTimeInput();
                                        bool dtor = view.PassDateBoundary(dateOfRelease);

                                        switch (dtor)
                                        {
                                            case true:
                                                view.DisplayMessage("\nEnter a new format identification number... ");
                                                List<Formats> formats = storageManager.GetAllFormats();
                                                int fUpper = storageManager.GetFormatBoundary();

                                                int formatId = view.GetIntInput();
                                                bool fid = view.PassBoundary(formatId, fUpper);

                                                switch (fid)
                                                {
                                                    case true:
                                                        view.DisplayMessage("\nEnter a new band identification number... ");
                                                        List<Bands> bands = storageManager.GetAllBands();
                                                        int bUpper = storageManager.GetBandBoundary();

                                                        view.DisplayBands(bands);

                                                        int bandId = view.GetIntInput();
                                                        bool bid = view.PassBoundary(bandId, bUpper);

                                                        switch (bid)
                                                        {
                                                            case true:
                                                                view.DisplayMessage("\nEnter a new shelf row identification number... ");
                                                                List<Rows> rows = storageManager.GetAllRows();
                                                                int rUpper = storageManager.GetRowBoundary();

                                                                int shelfRowId = view.GetIntInput();
                                                                bool sroid = view.PassBoundary(shelfRowId, rUpper);



                                                                switch (sroid)
                                                                {
                                                                    case true:
                                                                        int rowsAffected = storageManager.UpdateBandAlbumById(albumId, albumName, genreId, dateOfRelease, formatId, bandId, shelfRowId);
                                                                        view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                                                                    break;

                                                                    case false:
                                                                        

                                                                        BandAlbumPanel();

                                                                    break;

                                                                }

                                                            break;

                                                            case false:
                                                                

                                                                BandAlbumPanel();

                                                            break;
                                                        }

                                                        break;

                                                    case false:
                                                        

                                                        BandAlbumPanel();

                                                    break;

                                                }

                                            break;

                                            case false:
                                                

                                                BandAlbumPanel();

                                            break;

                                        }

                                        

                                    break;

                                    case false:
                                        

                                        BandAlbumPanel();

                                    break;
                                }

                            break;

                            case false:
                                

                                BandAlbumPanel();

                            break;
                            
                        }

                    break;

                    case false:
                        

                        BandAlbumPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandAlbumPanel();

            }

        }
        
        private static void InsertBandAlbum()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the new album... ");
                string albumName = view.GetInput();
                bool aln = view.PassRange(albumName.Length, 3, 50);

                int albumId = 0;

                switch (aln)
                {
                    case true:
                        view.DisplayMessage("\nEnter a genre identification number...");
                        List<Genres> genres = storageManager.GetAllGenres();
                        int gUpper = storageManager.GetGenreBoundary();

                        view.DisplayGenres(genres);

                        int genreId = view.GetIntInput();
                        bool gid = view.PassBoundary(genreId, gUpper);

                        switch (gid)
                        {
                            case true:
                                view.DisplayMessage("\nEnter the date of release... YYYY/MM/DD");

                                DateTime dateOfRelease = view.GetDateTimeInput();
                                bool dtor = view.PassDateBoundary(dateOfRelease);

                                switch (dtor)
                                {
                                    case true:
                                        view.DisplayMessage("\nEnter the format identification number... ");
                                        List<Formats> formats = storageManager.GetAllFormats();
                                        int fUpper = storageManager.GetFormatBoundary();

                                        int formatId = view.GetIntInput();
                                        bool fid = view.PassBoundary(formatId, fUpper);

                                        switch (fid)
                                        {
                                            case true:
                                                view.DisplayMessage("\nEnter the band identification number... ");
                                                List<Bands> bands = storageManager.GetAllBands();
                                                int bUpper = storageManager.GetBandBoundary();

                                                view.DisplayBands(bands);

                                                int bandId = view.GetIntInput();
                                                bool bid = view.PassBoundary(bandId, bUpper);

                                                switch (bid)
                                                {
                                                    case true:
                                                        view.DisplayMessage("\nEnter the shelf rows identification number... ");
                                                        List<Rows> rows = storageManager.GetAllRows();
                                                        int rUpper = storageManager.GetRowBoundary();

                                                        int shelfRowId = view.GetIntInput();
                                                        bool sroid = view.PassBoundary(shelfRowId, rUpper);

                                                        bool lost = false;

                                                        switch (sroid)
                                                        {
                                                            case true:
                                                                BandAlbums newAlbum = new BandAlbums(albumId, albumName, genreId, dateOfRelease, formatId, bandId, shelfRowId, lost);

                                                                int generatedId = storageManager.InsertBandAlbum(newAlbum);
                                                                view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                                                            break;

                                                            case false:
                                                                

                                                                BandAlbumPanel();

                                                            break;
                                                        }

                                                    break;

                                                    case false:
                                                        

                                                        BandAlbumPanel();

                                                    break;
                                                }

                                                break;

                                            case false:
                                                

                                                BandAlbumPanel();

                                            break;
                                        }

                                    break;

                                    case false:
                                        

                                        BandAlbumPanel();

                                    break;
                                }

                            break;

                            case false:
                                

                                BandAlbumPanel();

                            break;
                        }


                    break;

                    case false:
                        

                        BandAlbumPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandAlbumPanel();

            }
            
        }
        
        private static void DeleteBandAlbumById()
        {

            try
            {

                List<BandAlbums> albums = storageManager.GetAllBandAlbums();
                int alUpper = storageManager.GetBandAlbumBoundary();

                view.DisplayMessage("\nEnter the identification number... ");
                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);
           
                int BAlbumBReviewReference = storageManager.FetchBandReviewBandAlbumReferences(albumId);

                switch (alid)
                {
                    case true:

                        if (albumId.Equals(BAlbumBReviewReference))
                        {
                            view.DisplayReferentialError(wait);

                            BandAlbumPanel();

                        }

                        else 
                        { 
                            int albumssAffected = storageManager.DeleteBandAlbumById(albumId);
                            view.DisplayMessage($"\nDeleted {albumssAffected} albums.");


                        }
                        

                    break;

                    case false:
                        

                        BandAlbumPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
              
                view.DisplayMessage(e.Message);

                BandAlbumPanel();
    
            }

        }

        private static void MarkBandAsLost()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                List<BandAlbums> bands = storageManager.GetAllBandAlbums();
                int alUpper = storageManager.GetBandAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                bool lost = true;

                    switch (storageManager.FetchLostFromBandAlbums(albumId))
                    {
                        case true:
                            lost = false;

                        break;

                        case false:
                            lost = true;

                        break;
                    }

                switch (alid)
                {
                    case true:
                        int rowsAffected = storageManager.LostBand(albumId, lost);
                        view.DisplayMessage($"\nMarked/unmarked {rowsAffected} records as lost.");

                    break;

                    case false:
                        BandAlbumPanel();

                    break;
                }

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandAlbumPanel();

            }

        }


        // The Reviews.

        private static void ReviewPanel()
        {

            string reviewSelect;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            reviewSelect = view.DisplayEditingOptions("reviews", "album~variants");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {
                switch (reviewSelect)
                {
                    case "artists":
                        ArtistReviewPanel();

                        invalidCmd = false;

                    break;

                    case "bands":
                        BandReviewPanel();

                        invalidCmd = false;

                    break;

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        ReviewPanel();

                        invalidCmd = true;

                    break;

                }

            } while (invalidCmd);

        }

        private static void ArtistReviewReportPanel()
        {
            string aReviewCmd;
            int aReviewReportSelect;
            bool invalidCmd;

            try
            {
                aReviewReportSelect = view.DisplayReportOptions("artists", "reviews");

                Thread.Sleep(wait);
                Console.Clear();

                invalidCmd = false;

                do
                {
                    switch (aReviewReportSelect)
                    {
                        case 1:
                            storageManager.GetHighRankingArtistAlbums();

                            aReviewCmd = view.GetInput();

                            do
                            {
                                switch (aReviewCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistReviewReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistReviewReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);


                        break;

                        case 2:
                            storageManager.GetTopThreeFavouriteArtistAlbums();

                            aReviewCmd = view.GetInput();

                            do
                            {
                                switch (aReviewCmd.ToUpper())
                                {
                                    case "E":
                                        ArtistReviewReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        ArtistReviewReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);


                        break;

                        case 3:
                            ArtistReviewPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            ArtistReviewReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }
            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                ArtistReviewReportPanel();

            }            

        }

        private static void BandReviewReportPanel()
        {
            string bReviewCmd;
            int bReviewReportSelect;
            bool invalidCmd;

            try
            {
                bReviewReportSelect = view.DisplayReportOptions("bands", "reviews");

                Thread.Sleep(wait);
                Console.Clear();

                invalidCmd = false;

                do
                {
                    switch (bReviewReportSelect)
                    {
                        case 1:
                            storageManager.GetHighRankingBandAlbums();

                            bReviewCmd = view.GetInput();

                            do
                            {
                                switch (bReviewCmd.ToUpper())
                                {
                                    case "E":
                                        BandReviewReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandReviewReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);


                        break;

                        case 2:
                            storageManager.GetTopThreeFavouriteBandAlbums();

                            bReviewCmd = view.GetInput();

                            do
                            {
                                switch (bReviewCmd.ToUpper())
                                {
                                    case "E":
                                        BandReviewReportPanel();

                                        invalidCmd = false;

                                    break;

                                    default:
                                        view.DisplayError(wait);

                                        BandReviewReportPanel();

                                        invalidCmd = true;

                                    break;

                                }

                            } while (invalidCmd);


                        break;

                        case 3:
                            BandReviewPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            BandReviewReportPanel();

                            invalidCmd = true;

                        break;

                    }

                } while (invalidCmd);
            }
            catch (Exception e)
            {

                
                view.DisplayMessage(e.Message);

                Thread.Sleep(wait);
                Console.Clear();

                BandReviewReportPanel();

            }            

        }


        // The data-modifying commands for the Artist Albums Reviews table.

        private static void ArtistReviewPanel()
        {
            string aReviewCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllArtistReviews();

            aReviewCmd = view.DisplayEditingOptions("artist-album-reviews", "review~extras");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (aReviewCmd)
                {
                    case "up":
                        UpdateArtistReview();

                        invalidCmd = false;

                        ArtistReviewPanel();

                    break;

                    case "ins":
                        InsertArtistReview();

                        invalidCmd = false;

                        ArtistReviewPanel();

                    break;

                    case "del":
                        DeleteArtistReviewById();

                        invalidCmd = false;

                        ArtistReviewPanel();

                    break;

                    case "favourite":
                        FavouriteArtistReview();

                        invalidCmd = false;

                        ArtistReviewPanel();

                    break;

                    case "reports":
                        ArtistReviewReportPanel();

                        invalidCmd = false;

                    break;

                    case "search":
                        SearchArtistReviews();

                        invalidCmd = false;

                    break;

                    case "back":
                        ReviewPanel();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        ArtistReviewPanel();

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);
                
                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();
                int alUpper = storageManager.GetArtistAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter a ranking identification number...");
                        List<Tiers> tiers = storageManager.GetAllTiers();
                        int tUpper = storageManager.GetTierBoundary();

                        view.DisplayTiers(tiers);

                        int tierId = view.GetIntInput();
                        bool tid = view.PassBoundary(tierId, tUpper);

                        int personId = accountId;
 

                        switch (tid)
                        {
                            case true:
                                if (personId != storageManager.FetchAccountFromArtistReviews(reviewId))
                                {
                                    view.DisplayMessage("\nYou cannot modify someone elses review.");
                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    ArtistReviewPanel();

                                }

                                else
                                {
                                    int rowsAffected = storageManager.UpdateArtistReviewById(reviewId, albumId, personId, tierId);
                                    view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                                }

                            break;

                            case false:
                                

                                ArtistReviewPanel();

                            break;
                        }

                        break;

                    case false:
                        

                        ArtistReviewPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistReviewPanel();

                
            }

        }
        
        private static void InsertArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number...");
                List<ArtistAlbums> albums = storageManager.GetAllArtistAlbums();
                int alUpper = storageManager.GetArtistAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                int reviewId = 0;

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter a ranking identification number...");
                        List<Tiers> tiers = storageManager.GetAllTiers();
                        int tUpper = storageManager.GetTierBoundary();

                        view.DisplayTiers(tiers);

                        int tierId = view.GetIntInput();
                        bool tid = view.PassBoundary(tierId, tUpper);

                        switch (tid)
                        {
                            case true:
                                int personId = accountId;
  
                                bool favourite = false;

                                ArtistReviews newReviews = new ArtistReviews(reviewId, albumId, personId, tierId, favourite);

                                int generatedId = storageManager.InsertArtistReview(newReviews);
                                view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                            break;

                            case false:
                                

                                ArtistReviewPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        ArtistReviewPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistReviewPanel();
                
            }

        }
        
        private static void DeleteArtistReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId != storageManager.FetchAccountFromArtistReviews(reviewId))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    ArtistReviewPanel();

                }

                else
                {
                    int rowsAffected = storageManager.DeleteArtistReviewById(reviewId);
                    view.DisplayMessage($"\nDeleted {rowsAffected} row.");

                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistReviewPanel();
                
            }

        }

        private static void FavouriteArtistReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersArtistReviews(accountId);
                int reviewId = view.GetIntInput();

                

                if (accountId != storageManager.FetchAccountFromArtistReviews(reviewId))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. ");
                    Thread.Sleep(wait);
                    Console.Clear();

                    ArtistReviewPanel();

                }

                else
                {
                    
                    bool favourite = true;

                    switch (storageManager.FetchFavouriteFromArtistReviews(reviewId))
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
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                ArtistReviewPanel();
                
            }

        }


        // The data-modifying commands for the Band Albums Reviews table.

        private static void BandReviewPanel()
        {
            string bReviewCmd;
            bool invalidCmd = true;

            Thread.Sleep(wait);
            Console.Clear();

            storageManager.GetAllBandReviews();

            bReviewCmd = view.DisplayEditingOptions("band-album-reviews", "review~extras");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            do
            {

                switch (bReviewCmd)
                {
                    case "up":
                        UpdateBandReview();

                        invalidCmd = false;

                        BandReviewPanel();

                    break;

                    case "ins":
                        InsertBandReview();

                        invalidCmd = false;

                        BandReviewPanel();

                    break;

                    case "del":
                        DeleteBandReviewById();

                        invalidCmd = false;

                        BandReviewPanel();

                    break;

                    case "favourite":
                        FavouriteBandReview();

                        invalidCmd = false;

                        BandReviewPanel();

                    break;

                    case "reports":
                        BandReviewReportPanel();

                        invalidCmd = false;

                    break;

                    case "search":
                        SearchBandReviews();

                        invalidCmd = false;

                    break;

                    case "back":
                        ReviewPanel();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        BandReviewPanel();

                    break;
                }

            } while (invalidCmd);

        }

        private static void UpdateBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter a review identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);
                
                int reviewId = view.GetIntInput();

                view.DisplayMessage("\nEnter an album identification number...");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();
                int alUpper = storageManager.GetBandAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter a ranking identification number...");
                        List<Tiers> tiers = storageManager.GetAllTiers();
                        int tUpper = storageManager.GetTierBoundary();

                        view.DisplayTiers(tiers);

                        int tierId = view.GetIntInput();
                        bool tid = view.PassBoundary(tierId, tUpper);

                        int personId = accountId;


                        switch (tid)
                        {
                            case true:
                                if (personId != storageManager.FetchAccountFromBandReviews(reviewId))
                                {
                                    view.DisplayMessage("\nYou cannot modify someone elses review. ");
                                    Thread.Sleep(wait);
                                    Console.Clear();

                                    BandReviewPanel();

                                }

                                else
                                {
                                    int rowsAffected = storageManager.UpdateBandReviewById(reviewId, albumId, personId, tierId);
                                    view.DisplayMessage($"\nUpdated {rowsAffected} records.");

                                }

                            break;

                            case false:
                                

                                BandReviewPanel();

                            break;
                        }

                        break;

                    case false:
                        

                        BandReviewPanel();

                    break;

                }

            }

            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandReviewPanel();

                
            }

        }
        
        private static void InsertBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter an album identification number...");
                List<BandAlbums> albums = storageManager.GetAllBandAlbums();
                int alUpper = storageManager.GetBandAlbumBoundary();

                int albumId = view.GetIntInput();
                bool alid = view.PassBoundary(albumId, alUpper);

                int reviewId = 0;

                switch (alid)
                {
                    case true:
                        view.DisplayMessage("\nEnter a ranking identification number...");
                        List<Tiers> tiers = storageManager.GetAllTiers();
                        int tUpper = storageManager.GetTierBoundary();

                        view.DisplayTiers(tiers);

                        int tierId = view.GetIntInput();
                        bool tid = view.PassBoundary(tierId, tUpper);

                        switch (tid)
                        {
                            case true:
                                int personId = accountId;
  
                                bool favourite = false;

                                BandReviews newReviews = new BandReviews(reviewId, albumId, personId, tierId, favourite);

                                int generatedId = storageManager.InsertBandReview(newReviews);
                                view.DisplayMessage($"\nThe new identification number is: {generatedId}");

                            break;

                            case false:
                                

                                BandReviewPanel();

                            break;
                        }

                    break;

                    case false:
                        

                        BandReviewPanel();

                    break;
                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandReviewPanel();
                
            }

        }
        
        private static void DeleteBandReviewById()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId != storageManager.FetchAccountFromBandReviews(reviewId))
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review.");
                    Thread.Sleep(wait);
                    Console.Clear();

                    BandReviewPanel();

                }

                else
                {
                    int rowsAffected = storageManager.DeleteBandReviewById(reviewId);
                    view.DisplayMessage($"\nDeleted {rowsAffected} row.");

                }

            }
            
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandReviewPanel();
                
            }

        }

        private static void FavouriteBandReview()
        {
            

            try
            {
                view.DisplayMessage("\nEnter the identification number... ");
                int reviews = storageManager.GetUsersBandReviews(accountId);
                int reviewId = view.GetIntInput();



                if (accountId != storageManager.FetchAccountFromBandReviews(reviewId)) // If the review id connected to an accounts id does not correspond with the current users account id.
                {
                    view.DisplayMessage("\nYou cannot modify someone elses review. ");
                    Thread.Sleep(wait);
                    Console.Clear();

                     BandReviewPanel();

                }

                else
                {   
                    bool favourite = true;


                    switch (storageManager.FetchFavouriteFromBandReviews(reviewId))
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
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                BandReviewPanel();
                
            }

        }


        /*                                              *\  
         
               [UPDATES, INSERTS, DELETES AND MORE]
         
        \*                                              */

        /*
        
        THE IDEA:

            All methods used to update a field and/or fields are updated by calling the methods that are responsible for establishing a bridge between themselves and the database 
            when active. These methods are parameterised, and they are called to pass the inputs of the user through their instance. This applies to inserting and deleting as well.

            They are accessible by instantiating the Storage Manager class as a private static, where the same applies to the Console View.
          
        */

        // The Tiers.

        private static void TierPanel()
        {

            string tierCmd;
            bool invalidCmd;

            Thread.Sleep(wait);
            Console.Clear();

            List<Tiers> tiers = storageManager.GetAllTiers();
            view.DisplayTiers(tiers);

            tierCmd = view.DisplayEditingOptions("tiers", "none");
            view.DisplayMessage("");

            Thread.Sleep(wait);
            Console.Clear();

            invalidCmd = false;

            do
            {

                switch (tierCmd.ToLower())
                {

                    case "back":                                   
                        GoBack();

                        invalidCmd = false;

                    break;

                    default:
                        view.DisplayError(wait);

                        TierPanel();

                        invalidCmd = true;

                    break;
                }

            } while (invalidCmd);

        }

        
        // The search options.
        private static void SearchGenres()
        {
            string genreCmd;
            bool invalidCmd;


            try
            {
                view.DisplayMessage("\nEnter the name... \n");
                string genreSearch = view.GetInput();

                storageManager.SearchGenres(genreSearch);

                genreCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (genreCmd.ToUpper())
                    {
                        case "E":
                            GenrePanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchGenres();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchGenres();
    
            }

        }

        private static void SearchArtists()
        {
            string artistCmd;
            bool invalidCmd;

            try
            {
                view.DisplayMessage("\nEnter the name... \n");
                string artistSearch = view.GetInput();

                storageManager.SearchArtists(artistSearch);

                artistCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (artistCmd.ToUpper())
                    {
                        case "E":
                            ArtistPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                             SearchArtists();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchArtists();
    
            }

        }

        private static void SearchBands()
        {
            string bandCmd;
            bool invalidCmd;

            try
            {
                view.DisplayMessage("\nEnter the name... \n");
                string bandSearch = view.GetInput();

                storageManager.SearchBands(bandSearch);

                bandCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (bandCmd.ToUpper())
                    {
                        case "E":
                            BandPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchBands();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchBands();
    
            }

        }

        private static void SearchBandReviews()
        {

            try
            {
                string bReviewCmd;
                bool invalidCmd;

                view.DisplayMessage("\nEnter the ranking... \n");
                string bReviewSearch = view.GetInput();

                storageManager.SearchBandReviews(bReviewSearch);

                bReviewCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (bReviewCmd.ToUpper())
                    {
                        case "E":
                            BandReviewPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchBandReviews();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchBandReviews();
    
            }

        }

        private static void SearchArtistReviews()
        {

            try
            {
                string aReviewCmd;
                bool invalidCmd;

                view.DisplayMessage("\nEnter the ranking... \n");
                string aReviewSearch = view.GetInput();

                storageManager.SearchArtistReviews(aReviewSearch);


                aReviewCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (aReviewCmd.ToUpper())
                    {
                        case "E":
                            ArtistReviewPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchArtistReviews();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchArtistReviews();
    
            }

        }

        private static void SearchArtistAlbums()
        {

            try
            {
                string aAlbumCmd;
                bool invalidCmd;

                view.DisplayMessage("\nEnter the name... \n");
                string aAlbumSearch = view.GetInput();

                storageManager.SearchArtistAlbums(aAlbumSearch);

                aAlbumCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (aAlbumCmd.ToUpper())
                    {
                        case "E":
                            ArtistAlbumPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchArtistAlbums();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchArtistAlbums();
    
            }

        }

        private static void SearchBandAlbums()
        {

            try
            {
                string bAlbumCmd;
                bool invalidCmd;

                view.DisplayMessage("\nEnter the name... \n");
                string bAlbumSearch = view.GetInput();

                storageManager.SearchBandAlbums(bAlbumSearch);

                bAlbumCmd = view.GetInput();
                view.DisplayMessage("");

                Thread.Sleep(wait);

                do
                {
                    switch (bAlbumCmd.ToUpper())
                    {
                        case "E":
                            BandAlbumPanel();

                            invalidCmd = false;

                        break;

                        default:
                            view.DisplayError(wait);

                            SearchBandAlbums();

                            invalidCmd = true;

                        break;

                    }

                }  while (invalidCmd);

            }
            catch (Exception e)
            {
                
                view.DisplayMessage(e.Message);

                SearchBandAlbums();
    
            }

        }

    }

}
