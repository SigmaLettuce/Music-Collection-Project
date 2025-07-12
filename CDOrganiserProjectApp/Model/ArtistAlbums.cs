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
        public int GenreId { get; set; }
        public DateTime DateOfRelease { get; set; }
        public int FormatId { get; set; }
        public int ArtistId { get; set; }
        public int ShelfRowId { get; set; }

        public ArtistAlbums(int alid, string aln, int gid, DateTime dtor, int fid, int aid, int sroid)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreId = gid;
            DateOfRelease = dtor;
            FormatId = fid;
            ArtistId = aid;
            ShelfRowId = sroid;

        }
    }
}
