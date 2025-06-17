
namespace CDOrganiserProjectApp.Model
{
    public class Albums 
    { 
        public int AlbumId {  get; set; }
        public string AlbumName { get; set; }
        public string GenreName { get; set; }
        public string DateOfRelease { get; set; }
        public string FormatName { get; set; }
        public string ArtistName { get; set; }
        public string BandName { get; set; }
        public string RoomName { get; set; }
        public char ShelfTag { get; set; }

        public string ShelfRow { get; set; }
        public Albums(int alid, string aln, string gen, string dtor, string fn, string an, string bn, string rn, char sta, string sro)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreName = gen;
            DateOfRelease = dtor;
            FormatName = fn;
            ArtistName = an;
            BandName = bn;
            RoomName = rn;
            ShelfTag = sta;
            ShelfRow = sro;

        }
    }
}
