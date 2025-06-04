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
        public string DisplayMenu()
        {
            Console.WriteLine("\n\tWelcome to your CD Warehousing! ");
            Console.WriteLine("\tList of commands... \n");
            Console.WriteLine("Suffixes · Commands to run on sets of data");
            Console.WriteLine("\t'view' - Displays information");
            Console.WriteLine("\t'up' - Updates information");
            Console.WriteLine("\t'ins' - Adds new information");
            Console.WriteLine("\t'del' - Delete unwanted information");
            Console.WriteLine("\t'lost' - Marks a specified 'disc' as lost");
            Console.WriteLine("\t'search' - Search for a specific record\n");
            Console.WriteLine("Prefixes · Data sets available to run commands on");
            Console.WriteLine("\t'albums' - Albums of a compact disc; a CD");
            Console.WriteLine("\t'artists' - All artists who have published an album you own");
            Console.WriteLine("\t'bands' - All bands who have published an album you own");
            Console.WriteLine("\t'all' - All artists, including bands, who have published an album you own");
            Console.WriteLine("\t'rooms' - All available rooms");
            Console.WriteLine("\t'shelves' - All shelves; Identified by letters and paired with numbers to identify a specific row");
            Console.WriteLine("\t[name] - Any name of a specified record");

            return Console.ReadLine();
        }

        /* 
         Planning to deprecate the use of commands to specifically print for the 'Bands' table. Refer to pseudocodes and diagrams in booklet to plan.


        */

        public void DisplayBands(List<Bands> bands)
        {
            foreach (Bands band in bands)
            {
                Console.WriteLine($"{band.bandId}, {band.bandName}");
            }
                

        } 
        
        public void DisplayArtists(List<Artists> artists)
        {
            foreach (Artists artist in artists)
            {
                Console.WriteLine($"{artist.artistId}, {artist.artistName}");
            }

        }

        public void DisplayAlbums(List<Albums> albums)
        {
            foreach (Albums album in albums)
            {
                Console.WriteLine($"{album.albumId}, {album.albumName}, {album.genreName}, {album.dateOfRelease}");
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
            return int.Parse(Console.ReadLine());
        }



    }
}
