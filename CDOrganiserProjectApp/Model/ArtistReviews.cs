using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class ArtistReviews
    {
        public int ReviewId { get; set; }
        public int AlbumId { get; set; }
        public int PersonId { get; set; }
        public int TierId { get; set; }
        public bool Favourite { get; set; }

        public ArtistReviews(int rid, int alid, int pid, int tid, bool fv)
        {
            ReviewId = rid;
            AlbumId = alid;
            PersonId = pid;
            TierId = tid;
            Favourite = fv;

        }
    }
}
