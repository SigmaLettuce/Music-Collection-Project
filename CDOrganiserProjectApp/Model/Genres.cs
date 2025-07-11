using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Genres
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public Genres(int gid, string gn)
        { 
            GenreId = gid;
            GenreName = gn;
        }
    }
}
