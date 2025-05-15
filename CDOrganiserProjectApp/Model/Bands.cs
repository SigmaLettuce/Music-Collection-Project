

namespace CDOrganiserProjectApp.Model
{
    internal class Bands
    {

        public string bandName {  get; set; }   
        public string bandID { get; set; }  

        public Bands(string bn, string bid)
        {
            bandName = bn;
            bandID = bid;
        }
        
    }
}
