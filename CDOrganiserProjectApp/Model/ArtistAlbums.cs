using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class ArtistAlbums
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string GenreName { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string FormatName { get; set; }
        public string ArtistName { get; set; }
        public string ShelfRow { get; set; }
        public string Username { get; set; }
        public char TierTag { get; set; }
        public bool Favourite { get; set; }
        public bool Lost { get; set; }

        public ArtistAlbums(int alid, string aln, string gen, DateTime dtor, string fn, string an, string sro, string un, char tta, bool fv, bool l, params int[] foreignkeys)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreName = gen;
            DateOfRelease = dtor;
            FormatName = fn;
            ArtistName = an;
            ShelfRow = sro;
            Username = un;
            TierTag = tta;
            Favourite = fv;
            Lost = l;

        }
    }
}
