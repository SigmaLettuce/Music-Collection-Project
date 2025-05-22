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

        public int InsertBand(Bands bn)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Contents.tblBands (bandName) VALUES (@bandName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bn.bandName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandName(int bandId, string bandName)
        {
            string sqlStr = $"UPDATE Contents.tblBands SET bandName = @bandName WHERE bandID = @bandId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bandName);
                cmd.Parameters.AddWithValue("@bandId", bandId);
                return cmd.ExecuteNonQuery();
            }
        }

        
        public string DeleteBandByName(string bandName)
        {
            string sqlStr = "DELETE FROM Contents.tblBands WHERE bandName = @bandName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn)
            {
                cmds.Paramaters.AddWithValue("@bandName", bandName);
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
            string sqlStr = $"UPDATE Contents.tblArtists SET artistName = @artistName WHERE artistID = @artistId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                return cmd.ExecuteNonQuery();
            }
        }


    }
}
