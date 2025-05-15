
namespace CDOrganiserProjectApp.Model
{
    internal class Artists
    {

        public string artistName {  get; set; }
        public string artistId { get; set; }

        public Artists(string an, string aid)
        {
            artistName = an;
            artistId = aid;
        }

    }
}
