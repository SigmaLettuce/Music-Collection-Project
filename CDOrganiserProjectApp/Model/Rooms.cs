using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Rooms
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }

        public Rooms(string rid, string rn)
        {
            RoomId = rid;
            RoomName = rn;

        }

    }
}
