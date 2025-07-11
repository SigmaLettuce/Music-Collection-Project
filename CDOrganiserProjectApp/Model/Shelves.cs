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
        public string RoomName {  get; set; }

        public Shelves(char sta, string rn) 
        { 
            ShelfTag = sta;
            RoomName = rn;

        }
    }
}
