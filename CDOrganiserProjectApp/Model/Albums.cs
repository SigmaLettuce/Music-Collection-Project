
namespace CDOrganiserProjectApp.Model
{
    public class Albums
    { 
        public int albumId {  get; set; }
        public string albumName { get; set; }
        public string genreName { get; set; }
        public DateTime dateOfRelease { get; set; }

        public Albums(int alid, string aln, string gen, DateTime dtor)
        {
            albumId = alid;
            albumName = aln;
            genreName = gen;
            dateOfRelease = dtor;

        }
    }
}
