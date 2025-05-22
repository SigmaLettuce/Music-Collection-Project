using CDOrganiserProjectApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace CDOrganiserProjectApp
{
    // Manages connection to database, passes queries etc.
    public class StorageManager
    {
        private SqlConnection conn;

        public StorageManager(string connectionStr)
        {
            try
            {
                conn = new SqlConnection(connectionStr);
                conn.Open();
                Console.WriteLine("Connection successful.");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Connection failed.\n");
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.Message);
            }
        }


        public List<Bands> GetAllBands()
        {
            List<Bands> bands = new List<Bands>();
            string sqlStr = "SELECT * FROM Contents.tblBands";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        int bandId = Convert.ToInt32(reader["bandID"]);
                        string bandName = reader["bandName"].ToString();
                        bands.Add(new Bands(bandId, bandName)); 
                    }
                }
            }
            return bands;
        }

        public int UpdateBandName(int bandId, string bandName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE Contents.tblBands SET bandName = @bandName WHERE bandID = @bandId", conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bandName);
                cmd.Parameters.AddWithValue("@bandId", bandId);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Artists> GetAllArtists()
        {
            List<Artists> artists = new List<Artists>();
            string sqlStr = "SELECT * FROM Contents.tblArtists";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int artistId = Convert.ToInt32(reader["artistID"]);
                        string artistName = reader["artistName"].ToString();
                        artists.Add(new Artists(artistId, artistName));
                    }
                }
            }
            return artists;
        }

        public int UpdateArtistName(int artistId, string artistName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE Contents.tblArtists SET artistName = @artistName WHERE artistID = @artistId", conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                return cmd.ExecuteNonQuery();
            }
        }


    }
}
