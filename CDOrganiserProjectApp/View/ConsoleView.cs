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
        public string DisplayMenu()
        {
            Console.WriteLine("\nWelcome to your CD Warehousing! ");
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Display all records for bands?");
            Console.WriteLine("2. Update a bands name via. identification number?");
            Console.WriteLine("3. Insert a new band?");
            Console.WriteLine("4. Delete a record by name?");

            return Console.ReadLine();
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

    }
}
