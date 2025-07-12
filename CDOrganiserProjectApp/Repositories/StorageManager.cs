using CDOrganiserProjectApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace CDOrganiserProjectApp
{
    // Manages connection to database, passes queries etc.
    public class StorageManager
    {
        // A generically shared integer for delays.
        const int wait = 100; 

        // A private connection string that acts as a bridge to the database.
        private SqlConnection conn; 

        // A constructor with a connection string parameter to handle any connection related exceptions using exception handling. Displays the respective exception message as well as the handwritten. Any exceptions that are caught terminate the program.
        public StorageManager(string connectionStr)
        {

            try
            {
                conn = new SqlConnection(connectionStr);
                conn.Open();

                Console.WriteLine("\n  Connection successful.");
                
            }
            catch (SqlException e)
            {
                Console.WriteLine("\n  Connection failed.\n");

                Console.WriteLine("\n Please check GitHub for relevant information. ");
                Environment.Exit(0);
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n  An error occured.");

                Console.WriteLine("\n Please check GitHub for relevant information. ");
                Environment.Exit(0);
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
                cmd.Parameters.AddWithValue("@bandName", bands.BandName);
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
                cmd.Parameters.AddWithValue("@bandName", bands.BandName);
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
         
        public int InsertArtist(Artists artists)
        {
            string sqlStr = $"INSERT INTO Contents.tblArtists (artistName) VALUES (@artistName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand (sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistName", artists.ArtistName);
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


        public List<ArtistAlbums> GetAllArtistAlbums(int account)
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, username, tierTag, favourite, lost FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblRow, Properties.tblAccounts, Properties.tblTier WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.shelfRowID = tblRow.shelfRowID AND tblArtistAlbums.personID = tblAccounts.personID AND tblArtistAlbums.tierID = tblTier.tierID";
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
                        string shelfRow = reader["shelfRow"].ToString();
                        string username = reader["username"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        albums.Add(new ArtistAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, username, tierTag, favourite, lost));

                    }
                }
            }
            return albums;
        }

        public int InsertArtistAlbum(ArtistAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistAlbums (albumName, genreID, dateOfRelease, formatName, artistName, shelfRowID) VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @ArtistName, @ShelfRow); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreName", albums.GenreName);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatName", albums.FormatName);
                cmd.Parameters.AddWithValue("@ArtistName", albums.ArtistName);
                cmd.Parameters.AddWithValue("@ShelfRow", albums.ShelfRow);
              


                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public List<BandAlbums> GetAllBandAlbums()
        {
            List<BandAlbums> albums = new List<BandAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, bandName, shelfRow, username, tierTag, favourite, lost FROM Contents.tblGenres, Contents.tlbBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblRow, Properties.tblAccounts, Properties.tblTier WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.formatID = tblFormat.formatID AND tblBandAlbums.artistID = tblArtists.artistID AND tblBandAlbums.shelfRowID = tblRow.shelfRowID AND tblAccounts.personID = tblAccounts.personID AND tblBandAlbums.tierID = tblTier.tierID";
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
                        string shelfRow = reader["shelfRow"].ToString();
                        string username = reader["username"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        albums.Add(new BandAlbums(albumId, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, username, tierTag, favourite, lost));

                    }
                }
            }
            return albums;
        }
        
        public int InsertBandAlbum(BandAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBandAlbums (albumName, genreID, dateOfRelease, formatName, bandName, shelfRowID) VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @BandName, @ShelfRow); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreName", albums.GenreName);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatName", albums.FormatName);
                cmd.Parameters.AddWithValue("@BandName", albums.BandName);
                cmd.Parameters.AddWithValue("@ShelfRow", albums.ShelfRow);

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
            string sqlStr = $"INSERT INTO Properties.tblAccounts (fName, sName, username, pw, roleID) VALUES (@FirstName, @LastName, @Username, @Password, @RoleId); SELECT SCOPE_IDENTITY();";
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

        
        // Checks the username and password against themselves, then returns a match for a persons identification number in the database.
        public int FetchAccount(string un, string pw)
        {
            int personId = 0;

            string sqlStr = $"SELECT personID FROM Properties.tblAccounts WHERE username = @un OR pw = @pw";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@un", un);
                cmd.Parameters.AddWithValue("@pw", pw);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personId = Convert.ToInt32(reader["personID"]);
                    }
                }

                return personId;
            }

        }

        
        // Checks the username and password against themselves, then returns a match for a username in the database.
        public string FetchUsername(string un, string password)
        {
            string username = " ";

            string sqlStr = $"SELECT username FROM Properties.tblAccounts WHERE pw = @password OR username = @un";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@un", un);
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


        // Checks the username and password against themselves, then returns a match for a password in the database.
        public string FetchPassword(string username, string pw)
        {
            string password = " ";

            string sqlStr = $"SELECT pw FROM Properties.tblAccounts WHERE username = @username OR pw = @pw";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pw", pw);

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

        
        // Checks the username and password against themselves, then returns a match for a role in the database.
        public int FetchRole(string un, string pw)
        {

            int roleId = 0;

            string sqlStr = $"SELECT roleID FROM Properties.tblAccounts WHERE username = @un OR pw = @pw";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@un", un);
                cmd.Parameters.AddWithValue("@pw", pw);

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


        
        // Pulls data from the Formats table using the reader, and returns a list.
        public List<Formats> GetAllFormats()
        {
            List<Formats> formats = new List<Formats>();
            string sqlStr = "SELECT * FROM Properties.tblFormat";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        string formatName = reader["formatName"].ToString();
                        formats.Add(new Formats(formatId, formatName));
                    }
                }
            }
            return formats;
        }


        // Associates all the listed  with the insert; basically adds a new  
        public int InsertFormat(Formats formats)
        {
            string sqlStr = $"INSERT INTO Properties.tblFormat (formatName) VALUES (@FormatName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@FormatName", formats.FormatName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        
        // Associates the variables in the SQL with the string parameters, and returns the identification number. 
        public int UpdateFormatByName(string formatName, string newName)
        {
            string sqlStr = $"UPDATE Properties.tblFormat SET formatName = @newName WHERE formatName = @formatName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@formatName", formatName);
                cmd.Parameters.AddWithValue("@newName", newName);
                return cmd.ExecuteNonQuery();
            }
        }


        // Pulls data from the Formats table using the reader, and returns a list.
        public int DeleteFormatByName(string formatName)
        {
            string sqlStr = "DELETE FROM Properties.tblFormat WHERE formatName = @formatName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@formatName", formatName);
                return cmd.ExecuteNonQuery();
            }

        }


        public int Pagination(int totalPages)
        {
            int count = 0;

            int ifCount = 0;
            int pageNum = 1;

            count++;

            if (count % 10 == 0)
            {
                string input;

                ifCount++;
                Console.WriteLine("Navigate to the next page");
                Console.WriteLine("You are on page: " + pageNum + " of " + totalPages);

                input = Console.ReadLine();


                for (int i = 0; i < totalPages; i++)
                {
                    if (input.Equals(ifCount))
                    {
                        Console.WriteLine("E");
                    }
                }
            }

            return totalPages;
        }

        public void GetAllArtistsAndBands()
        {
            string sqlStr = "SELECT bandName as 'All Artists' FROM Contents.tblBands UNION SELECT artistName FROM Contents.tblArtists;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string allArtists = reader["All Artists"].ToString();

                        Console.WriteLine($"{allArtists}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }


        public void GetAToJArtists()
        {
            string sqlStr = "SELECT tblArtists.artistName, tblAlbums.albumName, tblAlbums.genreName FROM Contents.tblArtists, Contents.tblAlbums WHERE tblAlbums.artistID = tblArtists.artistID AND tblArtists.artistName LIKE '[A-J]%' ORDER BY tblArtists.artistName, tblAlbums.albumName, tblAlbums.genreName;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string artistName = reader["artistName"].ToString();
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();

                        Console.WriteLine($"{artistName}, {albumName}, {genreName}\n");
                        Thread.Sleep(wait);
                    }
                }
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
