using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class BandAlbums
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int GenreId { get; set; }
        public DateTime DateOfRelease { get; set; }
        public int FormatId { get; set; }
        public int BandId { get; set; }
        public int ShelfRowId { get; set; }
        public int PersonId { get; set; }
        public int TierId { get; set; }
        public bool Favourite { get; set; }
        public bool Lost { get; set; }

        public BandAlbums(int alid, string aln, int gid, DateTime dtor, int fid, int bid, int sroid, int pid, int tid, bool fv, bool l)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreId = gid;
            DateOfRelease = dtor;
            FormatId = fid;
            BandId = bid;
            ShelfRowId = sroid;
            PersonId = pid;
            TierId = tid;
            Favourite = fv;
            Lost = l;

        }
    }
}
