using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Shelves
    {
        public char ShelfTag { get; set; }
        public string ShelfRow { get; set; }

        public Shelves(char sta, string sro) 
        { 
            ShelfTag = sta;
            ShelfRow = sro;

        }
    }
}
