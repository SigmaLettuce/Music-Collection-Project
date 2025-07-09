using CDOrganiserProjectApp.Model;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.View
{
    public class ConsoleView 
    {

        const int wait = 100;
        
        public string StartMenu()
        {
            Console.WriteLine("\n\t CD Manager");
            Console.WriteLine("\n\t[/] 'Create account' - Press R");
            Console.WriteLine("\n\t[/] 'Log in' - Press L\n");

            return Console.ReadLine();
        }

        public string DisplayAdminMenu()
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! \n");
            Console.WriteLine("\tVIEW... ");

                Console.WriteLine("\n\t[/] 'albums' - Albums of a compact disc; a CD");
                Console.WriteLine("\t[/] 'artists' - All artists who have published an album you own");
                Console.WriteLine("\t[/] 'bands' - All bands who have published an album you own");
                Console.WriteLine("\t[/] 'all' - All artists, including bands, who have published an album you own");
                Console.WriteLine("\t[/] 'rooms' - All available rooms");
                Console.WriteLine("\t[/] 'shelves' - All available shelving units and their shelves\n");

            Console.WriteLine("\tSETTINGS... ");

                Console.WriteLine("\n\t[/] 'accounts' - Manage account permissions");
                Console.WriteLine("\t[/] 'help' - Takes you to the help page");
            
            Console.WriteLine("Enter any of the listings above for access... \n");


            
                
            return Console.ReadLine();
        }

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

            return Console.ReadLine();

        }

        public void DisplayHelp()
        {


        }

        public void DisplayBands(List<Bands> bands)
        {
            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.bandId}, {band.bandName}\n");
                Thread.Sleep(wait);

            }   

        }

        public void DisplayArtists(List<Artists> artists)
        {
            foreach (Artists artist in artists)
            {
                Console.WriteLine($"{artist.artistId}, {artist.artistName}\n");
                Thread.Sleep(wait);

            }

        }

        public void DisplayRooms(List<Rooms> rooms)
        {
            foreach (Rooms room in rooms)
            {
                Console.WriteLine($"{room.RoomId}, {room.RoomName}\n");
                Thread.Sleep(wait);

            }
            
        }

        public void DisplayArtistAlbums(List<ArtistAlbums> albums)
        {
            foreach (ArtistAlbums album in albums)
            {
                Console.WriteLine($"{album.AlbumId}, {album.AlbumName}, {album.GenreName}, {album.DateOfRelease.ToString("d")}, {album.FormatName}, {album.ArtistName}, {album.RoomName}, {album.ShelfTag}, {album.ShelfRow}, {album.Lost}\n");
                Thread.Sleep(wait);
                
            }
        }
        
        public void DisplayBandAlbums(List<BandAlbums> albums)
        {
            foreach (BandAlbums album in albums)
            {
                Console.WriteLine($"{album.AlbumId}, {album.AlbumName}, {album.GenreName}, {album.DateOfRelease.ToString("d")}, {album.FormatName}, {album.BandName}, {album.RoomName}, {album.ShelfTag}, {album.ShelfRow}, {album.Lost}\n");
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

        public char GetCharInput()
        {
            return Convert.ToChar(Console.ReadLine());
        }

        public DateTime GetDateTimeInput()
        {
            return Convert.ToDateTime(Console.ReadLine()).Date;
        }
    }
}
