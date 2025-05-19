
namespace CDOrganiserProjectApp.Model
{
    public class Artists
    {
        public int artistId { get; set; }
        public string artistName {  get; set; }


        public Artists(int aid, string an)
        {
            artistId = aid;
            artistName = an;

        }

    }
}
