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
        public int RoomId {  get; set; }

        public Shelves(char sta, int rid) 
        { 
            ShelfTag = sta;
            RoomId = rid;

        }
    }
}
