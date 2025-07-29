using CDOrganiserProjectApp.Model;

namespace CDOrganiserProjectApp.View
{
    public class ConsoleView 
    { 

        /*                     *\  
         
             [CONSOLE VIEW]
         
        \*                     */  


        // Global variable for the threadsleep.

        int wait = 100;

        /*
         
         The 'Start Menu.'
        Acts as a gateway to an account which holds a users unique history.

        */

        public string StartMenu()
        {
            Console.WriteLine("\n\t CD Manager");
            Console.WriteLine("\n\t     Create account - Press R + Enter");
            Console.WriteLine("\n\t     Log in - Press L + Enter\n");

            Console.WriteLine("\n\t     Help - Press H + Enter\n");

            // When the method is called, a prompt for the users input is returned. This let me store whatever is placed inside of the methods returned prompt in strings.

            return Console.ReadLine(); 
        }

        /*
        
         The 'Administrator Menu.'
        This is the Administrators menuscreen; the listings displayed are exclusive to the administrators of the application. A prompt for the users input is returned like the others.

        */ 

        public string DisplayAdminMenu()
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! \n");
            Console.WriteLine("\tVIEW... ");
            Console.WriteLine("\tThese are your available listings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'albums' - Albums of available CDs");
                Console.WriteLine("\t[/]  'reviews' - Reviews left by your friends");
                Console.WriteLine("\t[/]  'tiers' - All available ranks and their numerical value out of 10");
                Console.WriteLine("\t[/]  'artists' - All artists who have published an album you own");
                Console.WriteLine("\t[/]  'bands' - All bands who have published an album you own");
                Console.WriteLine("\t[/]  'genres' - All musical categories of your collection");
                Console.WriteLine("\t[/]  'formats' - All available disc formats");
                Console.WriteLine("\t[/]  'rooms' - All available rooms");
                Console.WriteLine("\t[/]  'rows' - All available rows of a shelf");
                Console.WriteLine("\t[/]  'shelves' - All available shelving units and their locations\n");

            Console.WriteLine("\tSETTINGS... ");
            Console.WriteLine("\tThese are your available settings, compiled into directories.");

                Console.WriteLine("\n\t[*]  'accounts' - Manage account permissions");
                Console.WriteLine("\t[*]  'help' - Takes you to the help page\n");

                Console.WriteLine("\n\t[*]  Log out - Press L + Enter\n");
            

            Console.WriteLine("Enter any of the listings above to gain access... \n");

            
            
                
            return Console.ReadLine();
        }

        /*
        
         The 'Guest Menu.'
        This is the Guests menuscreen; the listings displayed are readily available to all users of the application.

        */ 

        public string DisplayGuestMenu() 
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! \n");
            Console.WriteLine("\tVIEW... ");
            Console.WriteLine("\tThese are your available listings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'albums' - Albums of available CDs");
                Console.WriteLine("\t[/]  'artists' - All artists who have published an album you own");
                Console.WriteLine("\t[/]  'bands' - All bands who have published an album you own");
                Console.WriteLine("\t[/]  'genres' - All musical categories of your collection");

            Console.WriteLine("\n\tSETTINGS... ");
            Console.WriteLine("\tThese are your available settings, compiled into directories.");

                Console.WriteLine("\n\t[*]  'accounts' - Manage account permissions");
                Console.WriteLine("\t[*]  'help' - Takes you to the help page\n");

                Console.WriteLine("\n\t[*]  Log out - Press L + Enter\n");
            

            Console.WriteLine("Enter any of the listings above to gain access... \n");

            return Console.ReadLine();

        } 

        /*
          
         Display Editing Options.
        These are the editing options. The method is parameterized to evaluate which version of options a user gets.

        */

        public string DisplayEditingOptions(string d, string type)
        {
            if (type.Equals("default"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information\n");

                    Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("view~only"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'reports' - Opens the reports\n");

                    Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter 'reports' to gain access... \n");
            }

            else if (type.Equals("search~only"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'search' - Opens the search\n");

                    Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter 'reports' to gain access... \n");
            }

            else if (type.Equals("none"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");

                Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("You can't modify this listing. \n");

            }

            else if (type.Equals("default~extras"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'reports' - Opens the reports");
                    Console.WriteLine("\t[*]  'search' - Opens the search\n");

                    Console.WriteLine("\n\t[*] 'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("default~extras~search"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\t[*]  'search' - Opens the search\n");

                    Console.WriteLine("\n\t[*] 'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("default~extras~view"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'reports' - Opens the reports");

                    Console.WriteLine("\n\t[*] 'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("album~extras"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information");
                    Console.WriteLine("\t[/]  'lost' - Mark an album as lost\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'reports' - Opens the reports");
                    Console.WriteLine("\t[*]  'search' - Opens the search\n");

                    Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("album~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available album listings, compiled into directories.");

                    Console.WriteLine("\t[/]  'artists' - All the albums published by singular artists");
                    Console.WriteLine("\t[/]  'bands' - All the albums published by bands");

                Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("review~extras"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t[/]  'up' - Updates information");
                    Console.WriteLine("\t[/]  'ins' - Adds new information");
                    Console.WriteLine("\t[/]  'del' - Deletes unwanted information");
                    Console.WriteLine("\t[/]  'favourite' - Mark/unmark your favourite album\n");
                    
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[*]  'reports' - Opens the reports");
                    Console.WriteLine("\t[*]  'search' - Opens the search\n");
    
                    Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("account~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tThese are the creatable account-type listings, compiled into directories.");
                Console.WriteLine("\t[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

                    Console.WriteLine("\t[/]  'default' - Creates a default account");
                    Console.WriteLine("\t[/]  'admin' - Creates an admin account");

                Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to create ann account... \n");
            }

            else if (type.Equals("df~account~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tThese are the creatable account-type listings, compiled into directories.");
                Console.WriteLine("\t[x] WARNING: CREATING AN ACCOUNT BOOTS YOU TO THE LOGIN/REGISTER.\n");

                    Console.WriteLine("\t[/]  'default' - Creates a default account");

                Console.WriteLine("\n\t[*]  'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to create ann account... \n");
            }


            return Console.ReadLine();
        }

        /*

         Display Record Options.
        These are the record options. This method is parameterized to evaluate which version of options a user gets, similar to Display Editing.

        */

        public int DisplayRecordOptions(string d, string type)
        {
            if (type.Equals("artists"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[/]  'Release dates' - Enter 1\n");
                    Console.WriteLine("\n\t[/]  'A-J Artists' - Enter 2\n");
                    Console.WriteLine("\n\t[/]  'Early 2000s Music' - Enter 3\n");
                    Console.WriteLine("\n\t[/]  'All Albums Of A Genre' - Enter 4\n");
                    Console.WriteLine("\n\t[/]  'Both Artists And Bands' - Enter 5\n");
                    Console.WriteLine("\n\t[/]  'Total Albums Published By An An Artist' - Enter 6\n");
                    Console.WriteLine("\n\t[/]  'Total Publishes Per Year' - Enter 7\n");

                    Console.WriteLine("\n\t[*]  Enter 8 - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("bands"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[/]  'Release dates' - Enter 1\n");
                    Console.WriteLine("\n\t[/]  'A-J Bands' - Enter 2\n");
                    Console.WriteLine("\n\t[/]  'Early 2000s Music' - Enter 3\n");
                    Console.WriteLine("\n\t[/]  'All Albums Of A Genre' - Enter 4\n");
                    Console.WriteLine("\n\t[/]  'Both Artists And Bands' - Enter 5\n");
                    Console.WriteLine("\n\t[/]  'Total Albums Published By An An Band' - Enter 6\n");
                    Console.WriteLine("\n\t[/]  'Total Publishes Per Year' - Enter 7\n");

                    Console.WriteLine("\n\t[*]  Enter 8 - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("reviews"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[/]  'Highest ranked' - Enter 1\n");
                    Console.WriteLine("\n\t[/]  'Three Favourites' - Enter 2\n");

                    Console.WriteLine("\n\t[*]  Enter 3 - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("rows~shelves"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t[/]  'Total row occupancy' - Enter 1\n");

                Console.WriteLine("\n\t[*]  Enter 2 - Return to homepage\n");


                Console.WriteLine("You can't modify this listing. \n");

            }



            return Convert.ToInt32(Console.ReadLine());
        }


        /*
         
         Display Errors.
        A generic error prompt to avoid rewriting lines of code. Like many others, I can just call it in the defaults: of switch cases.
         
        */

        public void DisplayError(int delay)
        {
            Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? ");
            Thread.Sleep(delay);
            Console.Clear();

        }

        public void AltDisplayError(int delay)
        {
            Console.WriteLine("The input was invalid. Sending you back to homepage... ");
            Thread.Sleep(wait);
            Console.Clear();

        }

        /*
         
         The Support Page.
        This addresses some of the biggest issues a user might encounter when using this application that is out of my hands.
         
        */

        public string DisplayHelp(int delay)
        {
            Thread.Sleep(delay);
            Console.Clear();

            Console.WriteLine("\n\t [SUPPORT]\n");
            Console.WriteLine("\tFAQ... ");

                Console.WriteLine("\t[*]  'How can I login?' - Open the folder you installed, then open the .txt file 'account~list'.");
                Console.WriteLine("\t[*]  'Text from a record is clipping into the next line!' - Enter F11 or Alt + Enter.");
                Console.WriteLine("\t[*]  'Why does failing a command input send me back to the menuscreen?' - It was taking up too much of my time, couldn't wrap my head around it.");
                Console.WriteLine("\t[*]  'How do I view the listings?' - Exactly how you ended up here. Enter a listed keyword. This is not case-sensitive.");
                Console.WriteLine("\t[*]  'How do I leave the support page?' - Press E + Enter\n");

                Console.WriteLine("For more information, please refer to the .url file or the listed websites in the repository 'README.MD' for support. ");
                Console.WriteLine("");
                

            return Console.ReadLine();
        }      

        /*
        
         The Displays.
        These iterate through the database records being read and prints them.

        */

        public void DisplayBands(List<Bands> bands)
        {
            Console.WriteLine("ID:\tNAME:\n");

            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.BandId}\t{band.BandName}\n");
                Thread.Sleep(wait);

            }   
        }

        public void DisplayGenres(List<Genres> genres)
        {
            Console.WriteLine("ID:\tNAME:\n");

            foreach (Genres genre in genres)
            {
                Console.WriteLine($"{genre.GenreId}\t{genre.GenreName}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayTiers(List<Tiers> tiers)
        {
            Console.WriteLine("ID:\tCLASS:\tVALUE:\n");

            foreach (Tiers tier in tiers)
            {
                Console.WriteLine($"{tier.TierId}\t{tier.TierTag}\t{tier.TierNumericalValue}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayArtists(List<Artists> artists)
        {
            Console.WriteLine("ID:\tNAME:\n");

            foreach (Artists artist in artists)
            {
                Console.WriteLine($"{artist.ArtistId}\t{artist.ArtistName}\n");
                Thread.Sleep(wait);

            }

        }


        public void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        /*
          
         The Input Getters.
        Rather than retyping the readline, I can easily convert inputs inside of the main program.

        */ 

        public string GetInput()
        {
            return Console.ReadLine();
        }

        public int GetIntInput()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        public DateTime GetDateTimeInput()
        {
            return Convert.ToDateTime(Console.ReadLine());
        }

        public char GetCharInput()
        {
            return Convert.ToChar(Console.ReadLine());
        }


        public bool PassIntBoundary(int i)
        {
            bool boundary = i > 0;

            return boundary;
        }

    }
}
