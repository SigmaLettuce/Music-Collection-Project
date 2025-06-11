using CDOrganiserProjectApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

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

        public int InsertBand(Bands bands)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBands (bandName) VALUES (@bandName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bands.bandName);
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
       
        public int DeleteBandByName(string bandName)
        {
            string sqlStr = "DELETE FROM Contents.tblBands WHERE bandName = @bandName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bandName);
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
         
        public int InsertArtist(Artists artists)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtists (artistName) VALUES (@artistName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artists.artistName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistName(string artistName)
        {
            string sqlStr = $"UPDATE Contents.tblArtists SET artistName = @artistName WHERE artistName = @artistName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteArtistByName(string artistName)
        {
            string sqlStr = "DELETE FROM Contents.tblArtists WHERE artistName = @artistName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                return cmd.ExecuteNonQuery();
            }

        }


        public List<Albums> GetAllAlbums()
        {
            List<Albums> albums = new List<Albums>();
            string sqlStr = "SELECT * FROM Contents.tblAlbums";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        albums.Add(new Albums(albumId, albumName, genreName, dateOfRelease));
                      
                    }
                }
            }
            return albums;
        }

        /*
        public int InsertAlbum(Albums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblAlbums (albumName, genreName, dateOfRelease) VALUES (@albumName, @genreName, @dateOfRelease); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@albumName", albums.albumName, "@genreName", albums.genreName, "@dateOfRelease", albums.dateOfRelease);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateAlbumName(int albumId, string albumName, string genreName, string dateOfRelease)
        {
            string sqlStr = $"UPDATE Contents.tblAlbums SET albumName = @albumName WHERE artistID = @artistId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                return cmd.ExecuteNonQuery();
            }
        }

        */


        public void CloseConnection()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("\nConnection terminated.");
            }
        }
    }
}
