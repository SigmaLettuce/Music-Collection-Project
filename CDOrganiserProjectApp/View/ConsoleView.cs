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
            Console.WriteLine("\n\t 'Create account' - [R]");
            Console.WriteLine("\n\t 'Log in' - [L]\n");

            return Console.ReadLine();
        }

        public string DisplayAdminMenu()
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! ");
            Console.WriteLine("\tList of commands... \n");

            Console.WriteLine("Prefixes · Commands to run on sets of data");
            Console.WriteLine("\n\t'view' - Displays information");
            Console.WriteLine("\t'up' - Updates information");
            Console.WriteLine("\t'ins' - Adds new information");
            Console.WriteLine("\t'del' - Delete unwanted information");
            Console.WriteLine("\t'lost' - Marks a specified 'disc' as lost; this command can only be run on 'albums'\n");

            Console.WriteLine("Bases · Distinguishes data set variants");
            Console.WriteLine("\n\t'artist' - Albums of artists");
            Console.WriteLine("\t'band' - Albums of bands\n");

            Console.WriteLine("Suffixes · Data sets available to run commands on");
            Console.WriteLine("\n\t'albums' - Albums of a compact disc; a CD");
            Console.WriteLine("\t'artists' - All artists who have published an album you own");
            Console.WriteLine("\t'bands' - All bands who have published an album you own");
            Console.WriteLine("\t'all' - All artists, including bands, who have published an album you own");
            Console.WriteLine("\t'rooms' - All available rooms\n");

            Console.WriteLine("Extras · Commands irrelevant to your data");
            Console.WriteLine("\n\t'help' - A comprehensive guide to the appplication");
            Console.WriteLine("\t'create admin' - Creates an administrator account");
            Console.WriteLine("\t'create user' - Creates a user account");
            Console.WriteLine("\t'log out' - Exits the account; brings you back to 'Start Menu'");


            return Console.ReadLine();
        }

        public string DisplayGuestMenu() 
        {
            Console.WriteLine("\n\tWelcome to your CD Manager! ");
            Console.WriteLine("\tList of commands... \n"); 

            Console.WriteLine("Prefixes · Commands to run on sets of data");
            Console.WriteLine("\n\t'view' - Displays information\n");

            Console.WriteLine("Suffixes · Data sets available to run commands on");
            Console.WriteLine("\n\t'categories' - Categories of music\n");

            Console.WriteLine("Extras · Commands irrelevant to your data");
            Console.WriteLine("\t'create user' - Creates a user account");
            Console.WriteLine("\n\t'log out' - Exits the account; brings you back to 'Start Menu'");

            return Console.ReadLine();
        }

        /* 
         Planning to deprecate the use of commands to specifically print for the 'Bands' table. Refer to pseudocodes and diagrams in booklet to plan.


        */

        public void DisplayBands(List<Bands> bands)
        {
            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.bandId}, {band.bandName}");;
                Thread.Sleep(wait);

            }        

        }

        public void DisplayArtists(List<Artists> artists)
        {
            foreach (Artists artist in artists)
            {
                Console.WriteLine($"{artist.artistId}, {artist.artistName}");
                Thread.Sleep(wait);

            }

        }

        public void DisplayRooms(List<Rooms> rooms)
        {
            foreach (Rooms room in rooms)
            {
                Console.WriteLine($"{room.RoomId}, {room.RoomName}");
                Thread.Sleep(wait);

            }

        }

        public void DisplayArtistAlbums(List<ArtistAlbums> albums)
        {
            foreach (ArtistAlbums album in albums)
            {
                Console.WriteLine($"{album.AlbumId}, {album.AlbumName}, {album.GenreName}, {album.DateOfRelease}, {album.FormatName}, {album.ArtistName}, {album.RoomName}, {album.ShelfTag}, {album.ShelfRow}, {album.Lost}");
                Thread.Sleep(wait);
                
            }
        }
        
        public void DisplayBandAlbums(List<BandAlbums> albums)
        {
            foreach (BandAlbums album in albums)
            {
                Console.WriteLine($"{album.AlbumId}, {album.AlbumName}, {album.GenreName}, {album.DateOfRelease.ToString("d")}, {album.FormatName}, {album.BandName}, {album.RoomName}, {album.ShelfTag}, {album.ShelfRow}, {album.Lost}");
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
