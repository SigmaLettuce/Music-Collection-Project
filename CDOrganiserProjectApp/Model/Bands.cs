        
namespace CDOrganiserProjectApp.Model
{
    public class Bands
    {
        public int bandId { get; set; }  
        public string bandName {  get; set; }   

         
        public Bands(int bid, string bn)
        {
            bandId = bid;
            bandName = bn;

        }
        
    }
}
