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
        public string GenreName { get; set; }
        public string DateOfRelease { get; set; }
        public string FormatName { get; set; }
        public string BandName { get; set; }
        public string RoomName { get; set; }
        public char ShelfTag { get; set; }
        public string ShelfRow { get; set; }
        public bool Lost { get; set; }

        public BandAlbums(int alid, string aln, string gen, string dtor, string fn, string bn, string rn, char sta, string sro, bool l)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreName = gen;
            DateOfRelease = dtor;
            FormatName = fn;
            BandName = bn;
            RoomName = rn;
            ShelfTag = sta;
            ShelfRow = sro;
            Lost = l;

        }
    }
}
