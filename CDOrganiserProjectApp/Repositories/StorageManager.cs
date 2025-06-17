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

        public int UpdateBandByName(string bandName, string newName)
        {
            string sqlStr = $"UPDATE Contents.tblBands SET bandName = @newName WHERE bandName = @bandName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@bandName", bandName);
                cmd.Parameters.AddWithValue("@newName", newName);
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
            string sqlStr = $"INSERT INTO Contents.tblArtists (artistName) VALUES (@artistName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand (sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artists.artistName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistByName(string artistName, string newName)
        {
            string sqlStr = $"UPDATE Contents.tblArtists SET artistName = @newName WHERE artistName = @artistName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artistName);
                cmd.Parameters.AddWithValue("@newName", newName);
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

        public List<Rooms> GetAllRooms()
        {
            List<Rooms> rooms = new List<Rooms>();
            string sqlStr = "SELECT * FROM Properties.tblStorageRoom";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roomId = Convert.ToInt32(reader["roomID"]);
                        string roomName = reader["roomName"].ToString();
                        rooms.Add(new Rooms(roomId, roomName));
                    }
                }
            }
            return rooms;
        }

        public int InsertRoom(Rooms rooms)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Properties.tblStorageRoom (roomName) VALUES (@RoomName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@RoomName", rooms.RoomName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateRoomByName(string roomName, string newName)
        {
            string sqlStr = $"UPDATE Properties.tblStorageRoom SET roomName = @newName WHERE roomName = @roomName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@roomName", roomName);
                cmd.Parameters.AddWithValue("@newName", newName);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteRoomByName(string roomName)
        {
            string sqlStr = "DELETE FROM Properties.tblStorageRoom WHERE roomName = @roomName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@roomName", roomName);
                return cmd.ExecuteNonQuery();
            }

        }

        public List<ArtistAlbums> GetAllArtistAlbums()
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow, lost FROM Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblShelf, Properties.tblRow WHERE tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.shelfTag = tblShelf.shelfTag AND tblArtistAlbums.shelfRow = tblRow.shelfRow ORDER BY albumID asc";
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
                        string formatName = reader["formatName"].ToString();
                        string artistName = reader["artistName"].ToString();
                        string roomName = reader["roomName"].ToString();
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);
                        string shelfRow = reader["shelfRow"].ToString();
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        albums.Add(new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow, lost));

                    }
                }
            }
            return albums;
        }


        public int InsertArtistAlbum(ArtistAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistAlbums (albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow) VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @ArtistName, @RoomName, @ShelfTag, @ShelfRow); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreName", albums.GenreName);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatName", albums.FormatName);
                cmd.Parameters.AddWithValue("@ArtistName", albums.ArtistName);
                cmd.Parameters.AddWithValue("@RoomName", albums.RoomName);
                cmd.Parameters.AddWithValue("@ShelfTag", albums.ShelfTag);
                cmd.Parameters.AddWithValue("@ShelfRow", albums.ShelfRow);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public List<BandAlbums> GetAllBandAlbums()
        {
            List<BandAlbums> albums = new List<BandAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, bandName, roomName, shelfTag, shelfRow, lost FROM Contents.tblBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblStorageRoom WHERE tblBandAlbums.formatID = tblFormat.formatID AND tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.roomID = tblStorageRoom.roomID";
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
                        string formatName = reader["formatName"].ToString();
                        string bandName = reader["bandName"].ToString();
                        string roomName = reader["roomName"].ToString();
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);
                        string shelfRow = reader["shelfRow"].ToString();
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        albums.Add(new BandAlbums(albumId, albumName, genreName, dateOfRelease, formatName, bandName, roomName, shelfTag, shelfRow, lost));

                    }
                }
            }
            return albums;
        }
        

        public int InsertBandAlbum(BandAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblAlbums (albumName, genreName, dateOfRelease) VALUES (@albumName, @genreName, @dateOfRelease); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@albumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@genreName", albums.GenreName);
                cmd.Parameters.AddWithValue("@dateOfRelease", albums.DateOfRelease);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        /*
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

        public int CreateAccount(Accounts account)
        {
            string sqlStr = $"INSERT INTO Contents.tblAccounts (fName, sName, username, pw, roleID) VALUES (@FirstName, @LastName, @Username, @Password, @RoleId); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", account.FirstName);
                cmd.Parameters.AddWithValue("@LastName", account.LastName);
                cmd.Parameters.AddWithValue("@Username", account.Username);
                cmd.Parameters.AddWithValue("@Password", account.Password); 
                cmd.Parameters.AddWithValue("@RoleId", account.RoleId);

                return cmd.ExecuteNonQuery();
            }

        }


        public string GetUsernameCredentials(string password)
        {
            string username = " ";

            string sqlStr = $"SELECT username FROM Contents.tblAccounts WHERE pw = @password";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        username = reader["username"].ToString();
                    }

                }

                return username;
            }
        }

        public string GetPasswordCredentials(string username)
        {
            string password = " ";

            string sqlStr = $"SELECT pw FROM Contents.tblAccounts WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        password = reader["pw"].ToString();
                    }

                }

                return password;
            }
        }

        public int FetchRole(string password)
        {

            int roleId = 0;

            string sqlStr = $"SELECT roleID FROM Contents.tblAccounts WHERE pw = @password";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roleId = Convert.ToInt32(reader["roleID"].ToString());
                    }

                }

                return roleId;
            }
        }


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
