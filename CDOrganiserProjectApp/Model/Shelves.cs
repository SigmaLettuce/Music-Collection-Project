using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Shelves
    {
        public int ShelfTagId { get; set; }
        public char ShelfTag { get; set; }
        public int RoomId {  get; set; }

        public Shelves(int staid, char sta, int rid) 
        { 
            ShelfTagId = staid;
            ShelfTag = sta;
            RoomId = rid;

        }
    }
}
