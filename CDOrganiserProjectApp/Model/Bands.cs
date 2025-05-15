

namespace CDOrganiserProjectApp.Model
{
    internal class Bands
    {

        public string bandName {  get; set; }   
        public string bandId { get; set; }  
         
        public Bands(string bn, string bid)
        {
            bandName = bn;
            bandId = bid;
        }
        
    }
}
