using CDOrganiserProjectApp.Model;
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
        public void DisplayMenu()
        {
            Console.WriteLine("\nWelcome to your CD Warehousing! ");
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Display all records for bands?");
            Console.WriteLine("2. Display all records for artists?");
        }

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

        public string GetInput()
        {

            return Console.ReadLine();
        }

    }
}
