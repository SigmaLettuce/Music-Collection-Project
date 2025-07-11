        
namespace CDOrganiserProjectApp.Model
{
    public class Bands
    {
        public int BandId { get; set; }  
        public string BandName {  get; set; }   

         
        public Bands(int bid, string bn)
        {
            BandId = bid;
            BandName = bn;

        }
        
    }
}
