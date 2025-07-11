
namespace CDOrganiserProjectApp.Model
{
    public class Artists
    {
        public int ArtistId { get; set; }
        public string ArtistName {  get; set; }


        public Artists(int aid, string an)
        {
            ArtistId = aid;
            ArtistName = an;

        }

    }
}
