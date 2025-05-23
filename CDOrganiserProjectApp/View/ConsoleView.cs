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
            Console.WriteLine("\nWelcome to your CD Warehousing! ");
            Console.WriteLine("List of commands... \n");
            Console.WriteLine("'view' - Display all records for bands?");
            Console.WriteLine("'up' - Update a bands name via. identification number?");
            Console.WriteLine("'ins' - Insert a new band?");
            Console.WriteLine("'del' - Delete a record by name?\n");

            return GetInput();
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
