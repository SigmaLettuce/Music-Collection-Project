
namespace CDOrganiserProjectApp.Model
{
    public class Albums 
    { 
        public int AlbumId {  get; set; }
        public string AlbumName { get; set; }
        public string GenreName { get; set; }
        public string DateOfRelease { get; set; }
        public int FormatId { get; set; }
        public int ArtistId { get; set; }
        public int BandId { get; set; }
        public int RoomId { get; set; }
        public char ShelfTag { get; set; }

        public string ShelfRow { get; set; }
        public Albums(int alid, string aln, string gen, string dtor, int fid, int aid, int bid, int rid, char sta, string sro)
        {
            AlbumId = alid;
            AlbumName = aln;
            GenreName = gen;
            DateOfRelease = dtor;
            FormatId = fid;
            ArtistId = aid;
            BandId = bid;
            RoomId = rid;
            ShelfTag = sta;
            ShelfRow = sro;

        }
    }
}
