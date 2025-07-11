using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Rooms
    {
        public string ShelfTag { get; set; }
        public string RoomName { get; set; }

        public Rooms(string sta, string rn)
        {
            ShelfTag = sta;
            RoomName = rn;

        }

    }
}
