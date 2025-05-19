        
namespace CDOrganiserProjectApp.Model
{
    public class Bands
    {

        public string bandName {  get; set; }   
        private int bandId { get; set; }  
         
        public Bands(string bn, int bid)
        {
            bandName = bn;
            bandId = bid;
        }
        
    }
}
