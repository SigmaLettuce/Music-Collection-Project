using Azure;
using CDOrganiserProjectApp.Model;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Linq;

namespace CDOrganiserProjectApp.View
{
    public class ConsoleView 
    {

        const int wait = 100;

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

            // When the method is called, a prompt for the users input is reciprocated. This let me store whatever is placed inside of the methods returned prompt in strings.

            return Console.ReadLine(); 
        }

        /*
        
         The 'Administrator Menu.'
        This is the Administrators menuscreen; the listings displayed are exclusive to the administrators of the application.

        */ 

        public string DisplayAdminMenu()
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! \n");
            Console.WriteLine("\tVIEW... ");
            Console.WriteLine("\tThese are your available listings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'albums' - Albums of a compact disc; a CD");
                Console.WriteLine("\t[/]  'artists' - All artists who have published an album you own");
                Console.WriteLine("\t[/]  'bands' - All bands who have published an album you own");
                Console.WriteLine("\t[/]  'genres' - All musical categories of your collection");
                Console.WriteLine("\t[/]  'rooms' - All available rooms");
                Console.WriteLine("\t[/]  'shelves' - All available shelving units and their locations\n");

            Console.WriteLine("\tSETTINGS... ");
            Console.WriteLine("\tThese are your available settings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'accounts' - Manage account permissions");
                Console.WriteLine("\t[/]  'help' - Takes you to the help page\n");

                Console.WriteLine("\n\t     Log out - Press L + Enter\n");
            

            Console.WriteLine("Enter any of the listings above to gain access... \n");


            
                
            return Console.ReadLine();
        }

        /*
        
         The 'Guest Menu.'
        This is the Guests menuscreen; the listings displayed are readily available to all users of the application.

        */ 

        public string DisplayGuestMenu() 
        {
            /*
            Console.WriteLine("\n\tWelcome to your CD Manager! ");
            Console.WriteLine("\tList of commands... \n"); 

            Console.WriteLine("Prefixes · Commands to run on sets of data");
            Console.WriteLine("\n\t'view' - Displays information\n");

            Console.WriteLine("Suffixes · Data sets available to run commands on");
            Console.WriteLine("\n\t'categories' - Categories of music\n");

            Console.WriteLine("Extras · Commands irrelevant to your data");
            Console.WriteLine("\n\tPrefixes · Commands to run on sets of data");
            Console.WriteLine("\t'create' - Creates an account");
            Console.WriteLine("\t'del' - Deletes an account\n");

            Console.WriteLine("Suffixes · Data sets available to run commands on");
            Console.WriteLine("\n\t'user' - A user account");
            Console.WriteLine("\t'help' - A comprehensive guide to the appplication");
            Console.WriteLine("\t'log out' - Exits the account; brings you back to 'Start Menu'\n");
            */

            Console.WriteLine("\n\tWelcome to your CD Manager! \n");
            Console.WriteLine("\tVIEW... ");
            Console.WriteLine("\tThese are your available listings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'albums' - Albums of a compact disc; a CD");
                Console.WriteLine("\t[/]  'artists' - All artists who have published an album you own");
                Console.WriteLine("\t[/]  'bands' - All bands who have published an album you own");
                Console.WriteLine("\t[/]  'genres' - All musical categories of your collection");

            Console.WriteLine("\tSETTINGS... ");
            Console.WriteLine("\tThese are your available settings, compiled into directories.");

                Console.WriteLine("\n\t[/]  'accounts' - Manage account permissions");
                Console.WriteLine("\t[/]  'help' - Takes you to the help page\n");

                Console.WriteLine("\n\t     Log out - Press l + Enter\n");
            

            Console.WriteLine("Enter any of the listings above to gain access... \n");

            return Console.ReadLine();

        } 

        public string DisplayEditingOptions(string d, string type)
        {
            if (type.Equals("default"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t'up' - Updates information");
                    Console.WriteLine("\t'ins' - Adds new information");
                    Console.WriteLine("\t'del' - Deletes unwanted information\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t'reports' - Opens the reports\n");

                    Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("view~only"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t'reports' - Opens the reports\n");

                    Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter 'reports' to gain access... \n");
            }

            else if (type.Equals("none"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");

                Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("You can't modify this listing. \n");

            }


            else if (type.Equals("album~extras"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tYou can now modify this listing.");

                    Console.WriteLine("\n\t'up' - Updates information");
                    Console.WriteLine("\t'ins' - Adds new information");
                    Console.WriteLine("\t'del' - Deletes unwanted information\n");
                    Console.WriteLine("\t'favourite' - Mark your favourite album\n");
                    Console.WriteLine("\t'lost' - Mark an album as lost\n");

                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available report listings, compiled into directories.");

                    Console.WriteLine("\n\t'reports' - Opens the reports\n");

                    Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("album~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tVIEW... ");
                Console.WriteLine("\tThese are the available album listings, compiled into directories.");

                    Console.WriteLine("\t[/]  'artists' - All the albums published by singular artists");
                    Console.WriteLine("\t[/]  'bands' - All the albums published by bands");

                Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to gain access... \n");
            }

            else if (type.Equals("account~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tThese are the creatable account-type listings, compiled into directories.");

                    Console.WriteLine("\t[/]  'default' - Creates a default account");
                    Console.WriteLine("\t[/]  'admin' - Creates an admin account");

                Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to create ann account... \n");
            }

            else if (type.Equals("df~account~variants"))
            {
                Console.WriteLine($"\n\t [CD MANAGER / {d}]\n");
                Console.WriteLine("\tEDIT... ");
                Console.WriteLine("\tThese are the creatable account-type listings, compiled into directories.");

                    Console.WriteLine("\t[/]  'default' - Creates a default account");

                Console.WriteLine("\n\t'back' - Return to homepage\n");


                Console.WriteLine("Enter any of the listings above to create ann account... \n");
            }


                return Console.ReadLine();
        }

        public string ConfigureOption()
        {
            Console.WriteLine("Are you sure... ? Y/n");

            return Console.ReadLine();
        }

        public void DisplayError(int delay)
        {
            Console.WriteLine("I'm sorry, this isn't a valid selection. Can you try again? ");
            Thread.Sleep(delay);
            Console.Clear();

        }

        public string DisplayHelp()
        {
            Console.WriteLine("\n\t [SUPPORT]");
            Console.WriteLine("\tFAQ... ");

                Console.WriteLine("\t[/]  'How can I login?' - Open the folder you installed, then open the .txt file 'account~list'.");
                Console.WriteLine("\t[/]  'The connection isn't being established!' - Open the .sln in an IDE > Open the SQL Server Object Explorer. If that doesn't fix it, please open the repository .url.");
                Console.WriteLine("\t[/]  'Text from a record is clipping into the next line!' - Enter F11 or Alt + Enter.");
                Console.WriteLine("\t[/]  'How do I view the listings?' - Exactly how you ended up here. Enter a listed keyword. This is not case-sensitive.");
                Console.WriteLine("\t[/]  'How do I leave the support page?' - Press E + Enter");

                Console.WriteLine("For more information, please refer to the .url file or the listed websites in the repository 'README.MD' for support. ");

                

            return Console.ReadLine();
        }

        public IList<Bands> BandPager(IList<Bands> bands, int pg, int pgsz)
        {
            

            return bands.Skip(pg - 1 * pgsz).Take(pgsz).ToList();
        }

        public void DisplayBands(List<Bands> bands)
        {
            Console.WriteLine("ID: NAME: ");

            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.BandId}, {band.BandName}\n");
                Thread.Sleep(wait);

            }   
        }

        public void DisplayGenres(List<Genres> genres)
        {
            Console.WriteLine("ID: NAME: ");

            foreach (Genres genre in genres)
            {
                Console.WriteLine($"{genre.GenreId}, {genre.GenreName}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayTiers(List<Tiers> tiers)
        {
            Console.WriteLine("ID: CLASS: VALUE:");

            foreach (Tiers tier in tiers)
            {
                Console.WriteLine($"{tier.TierId}, {tier.TierTag}, {tier.TierNumericalValue}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayArtists(List<Artists> artists)
        {
            Console.WriteLine("ID: NAME: ");

            foreach (Artists artist in artists)
            {
                Console.WriteLine($"{artist.ArtistId}, {artist.ArtistName}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayRooms(List<Rooms> rooms)
        {
            Console.WriteLine("ID: NAME: ");

            foreach (Rooms room in rooms)
            {
                Console.WriteLine($"{room.RoomId}, {room.RoomName}\n");
                Thread.Sleep(wait);

            }
            
        }
        public void DisplayShelves(List<Shelves> shelves)
        {
            Console.WriteLine("ID: NAME: ");

            foreach (Shelves shelf in shelves)
            {
                Console.WriteLine($"{shelf.ShelfTag}, {shelf.RoomName}\n");
                Thread.Sleep(wait);

            }
            
        }

        public void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

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

    }
}
