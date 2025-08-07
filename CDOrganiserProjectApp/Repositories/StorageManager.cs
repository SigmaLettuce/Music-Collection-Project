using CDOrganiserProjectApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;


namespace CDOrganiserProjectApp
{

    // Manages connection to database, passes queries etc.
    public class StorageManager
    {

        /*                      *\  
         
            [STORAGE MANAGER]
         
        \*                      */
        

        // A generically shared integer for delays.
        int wait = 100; 

        // A private connection string that acts as a bridge to the database.
        private SqlConnection conn; 

        // A  with a connection string parameter to handle any connection related exceptions using exception handling. Displays the respective exception message as well as the handwritten. Any exceptions that are caught terminate the program.
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

                Console.WriteLine("\n Please check GitHub or folder for relevant information. ");
                Environment.Exit(0);
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n  An error occured.");

                Console.WriteLine("\n Please check GitHub or folder for relevant information. ");
                Environment.Exit(0);
                Console.WriteLine(ex.Message);
            }
        }

        public int GetBandBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 bandID FROM Contents.tblBands ORDER BY bandID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["bandID"]);
                    }
                }
            }

            return max;

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

        public int UpdateBandById(int bandId, string bandName)
        {
            string sqlStr = $"UPDATE Contents.tblBands SET bandName = @bandName WHERE bandID = @bandId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@bandId", bandId);
                cmd.Parameters.AddWithValue("@bandName", bandName);
                return cmd.ExecuteNonQuery();
            }
        }
       
        public int DeleteBandById(int bandId)
        {
            string sqlStr = "DELETE FROM Contents.tblBands WHERE bandID = @bandId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@bandId", bandId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetGenreBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 genreID FROM Contents.tblGenres ORDER BY genreID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["genreID"]);
                    }
                }
            }

            return max;

        }

        public List<Genres> GetAllGenres()
        {
            List<Genres> genres = new List<Genres>();
            string sqlStr = "SELECT * FROM Contents.tblGenres";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        int genreId = Convert.ToInt32(reader["genreID"]);
                        string genreName = reader["genreName"].ToString();
                        genres.Add(new Genres(genreId, genreName)); 
                    }
                }
            }
            return genres;
        }

        public int InsertGenre(Genres genres)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblGenres (genreName) VALUES (@genreName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@genreName", genres.GenreName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateGenreById(int genreId, string genreName)
        {
            string sqlStr = $"UPDATE Contents.tblGenres SET genreName = @genreName WHERE genreID = @genreId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@genreName", genreName);
                return cmd.ExecuteNonQuery();
            }
        }
       
        public int DeleteGenreById(int genreId)
        {
            string sqlStr = "DELETE FROM Contents.tblGenres WHERE genreID = @genreID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@genreID", genreId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetTierBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 tierID FROM Properties.tblTier ORDER BY tierID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["tierID"]);
                    }
                }
            }

            return max;

        }

        public List<Tiers> GetAllTiers()
        {
            List<Tiers> tiers = new List<Tiers>();
            string sqlStr = "SELECT * FROM Properties.tblTier";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        int tierId = Convert.ToInt32(reader["tierID"]);
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        int tagValue = Convert.ToInt32(reader["tierNumericalValue"]);
                        tiers.Add(new Tiers(tierId, tierTag, tagValue)); 
                    }
                }
            }
            return tiers;
        }


        public int GetArtistBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 artistID FROM Contents.tblArtists ORDER BY artistID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["artistID"]);
                    }
                }
            }

            return max;

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
                cmd.Parameters.AddWithValue("@artistName", artists.ArtistName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistById(int artistId, string artistName)
        {
            string sqlStr = $"UPDATE Contents.tblArtists SET artistName = @artistName WHERE artistID = @artistId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistId", artistId);
                cmd.Parameters.AddWithValue("@artistName", artistName);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteArtistById(int artistId)
        {
            string sqlStr = "DELETE FROM Contents.tblArtists WHERE artistID = @artistId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistId", artistId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetRoomBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 roomID FROM Properties.tblStorageRoom ORDER BY roomID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["roomID"]);
                    }
                }
            }

            return max;

        }

        public List<Rooms> GetAllRooms()
        {
            List<Rooms> rooms = new List<Rooms>();
            string sqlStr = "SELECT * FROM Properties.tblStorageRoom";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                Console.WriteLine("ID:\tROOM:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roomId = Convert.ToInt32(reader["roomID"]);
                        string roomName = reader["roomName"].ToString();

                        rooms.Add(new Rooms(roomId, roomName));

                        Console.WriteLine($"{roomId}\t{roomName}\n");
                        Thread.Sleep(wait);
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

        public int UpdateRoomById(int roomId, string roomName)
        {
            string sqlStr = $"UPDATE Properties.tblStorageRoom SET roomName = @roomName WHERE roomID = @roomId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@roomId", roomId);
                cmd.Parameters.AddWithValue("@roomName", roomName);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteRoomById(int roomId)
        {
            string sqlStr = "DELETE FROM Properties.tblStorageRoom WHERE roomID = @roomId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@roomId", roomId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetShelfBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 shelfTagID FROM Properties.tblShelf ORDER BY shelfTagID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["shelfTagID"]);
                    }
                }
            }

            return max;

        }

        public List<Shelves> GetAllShelves()
        {
            List<Shelves> shelves = new List<Shelves>();
            string sqlStr = "SELECT tblShelf.shelfTagID, tblShelf.shelfTag, tblStorageRoom.roomName FROM Properties.tblShelf, Properties.tblStorageRoom WHERE tblShelf.roomID = tblStorageRoom.roomID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tTAG:\tROOM:\n");

                    while (reader.Read())
                    {
                        int shelfTagId = Convert.ToInt32(reader["shelfTagID"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);
                        string roomName = reader["roomName"].ToString();

                        Console.WriteLine($"{shelfTagId}\t{shelfTag}\t{roomName}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return shelves;
        }

        public int InsertShelf(Shelves shelves)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Properties.tblShelf (shelfTag, roomID) VALUES (@shelfTag, @roomId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@shelfTag", shelves.ShelfTag);
                cmd.Parameters.AddWithValue("@roomId", shelves.RoomId);
                
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateShelfRoomById(int shelfTagId, int roomId)
        {
            string sqlStr = $"UPDATE Properties.tblShelf SET roomID = @roomId WHERE shelfTagID = @shelfTagId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfTagId", shelfTagId);
                cmd.Parameters.AddWithValue("@roomId", roomId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteShelfById(int shelfTagId)
        {
            string sqlStr = "DELETE FROM Properties.tblShelf WHERE shelfTagID = @shelfTagId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfTagId", shelfTagId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetRowBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 shelfRowID FROM Properties.tblRow ORDER BY shelfRowID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["shelfRowID"]);
                    }
                }
            }

            return max;

        }

        public List<Rows> GetAllRows()
        {
            List<Rows> rows = new List<Rows>();
            string sqlStr = "SELECT shelfRowID, shelfRow, shelfTag FROM Properties.tblRow, Properties.tblShelf WHERE tblRow.shelfTagID = tblShelf.shelfTagID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tROW:\tTAG:\n");

                    while (reader.Read())
                    {
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        int shelfRow = Convert.ToInt32(reader["shelfRow"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);

                        Console.WriteLine($"{shelfRowId}\t{shelfRow}\t{shelfTag}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return rows;
        }

        public int InsertRow(Rows rows)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Properties.tblRow (shelfRow, shelfTagID) VALUES (@shelfRow, @shelfTagId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@shelfRow", rows.ShelfRow);
                cmd.Parameters.AddWithValue("@shelfTagId", rows.ShelfTagId);
                
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateRowsShelfById(int shelfRowId, int shelfTagId)
        {
            string sqlStr = $"UPDATE Properties.tblRow SET shelfTagID = @shelfTagId WHERE shelfRowID = @shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);
                cmd.Parameters.AddWithValue("@shelfTagId", shelfTagId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteRowById(int shelfRowId)
        {
            string sqlStr = "DELETE FROM Properties.tblRow WHERE shelfRowID = @shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetArtistAlbumBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 albumID FROM Contents.tblArtistAlbums ORDER BY albumID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["albumID"]);
                    }
                }
            }

            return max;

        }

        public List<ArtistAlbums> GetAllArtistAlbums()
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, shelfTag, shelfRow, lost FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblShelf, Properties.tblRow WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblShelf.shelfTagID = tblRow.shelfTagID AND tblArtistAlbums.shelfRowID = tblRow.shelfRowID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tCATEGORY:\tRELEASE DATE:\tFORMAT:\tARTIST:\tTAG:\tROW:\tFAVOURITE:\n");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        string formatName = reader["formatName"].ToString();
                        string artistName = reader["artistName"].ToString();
                        string shelfTag = reader["shelfTag"].ToString();
                        string shelfRow = reader["shelfRow"].ToString();     
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        Console.WriteLine($"{albumId}\t{albumName}\t{genreName}\t{dateOfRelease.ToString("d")}\t{formatName}\t{artistName}\t{shelfTag}\t{shelfRow}\t{lost}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }

        public int InsertArtistAlbum(ArtistAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistAlbums (albumName, genreID, dateOfRelease, formatID, artistID, shelfRowID, lost) VALUES (@AlbumName, @GenreId, @DateOfRelease, @FormatId, @ArtistId, @ShelfRowId, @Lost); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@ArtistId", albums.ArtistId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);
                cmd.Parameters.AddWithValue("@Lost", albums.Lost);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistAlbumById(int albumId, string albumName, int genreId, DateTime dateOfRelease, int formatId, int artistId, int shelfRowId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, artistID = @artistId, shelfRowId = @shelfRowId, lost = @lost WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@albumName", albumName);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@dateOfRelease", dateOfRelease);
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);
                cmd.Parameters.AddWithValue("@lost", lost);

                return cmd.ExecuteNonQuery();
            }
        }
        
        public int LostArtist(int albumId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET lost = @lost WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@lost", lost);                

                return cmd.ExecuteNonQuery();
            }
        }

        public int FavouriteArtist(int reviewId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblArtistReviews SET favourite = @favourite WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                cmd.Parameters.AddWithValue("@favourite", favourite);                

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteArtistAlbumById(int albumId)
        {
            string sqlStr = "DELETE FROM Contents.tblArtistAlbums WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetBandAlbumBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 albumID FROM Contents.tblBandAlbums ORDER BY albumID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["albumID"]);
                    }
                }
            }

            return max;

        }

        public List<BandAlbums> GetAllBandAlbums()
        {
            List<BandAlbums> albums = new List<BandAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, bandName, shelfTag, shelfRow, lost FROM Contents.tblGenres, Contents.tblBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblShelf, Properties.tblRow WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.formatID = tblFormat.formatID AND tblBandAlbums.bandID = tblBands.bandID AND tblShelf.shelfTagID = tblRow.shelfTagID AND tblBandAlbums.shelfRowID = tblRow.shelfRowID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tCATEGORY:\tRELEASE DATE:\tFORMAT:\tARTIST:\tTAG:\tROW:\tFAVOURITE:\n");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        string formatName = reader["formatName"].ToString();
                        string bandName = reader["bandName"].ToString();
                        string shelfTag = reader["shelfTag"].ToString();
                        string shelfRow = reader["shelfRow"].ToString();     
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        Console.WriteLine($"{albumId}\t{albumName}\t{genreName}\t{dateOfRelease.ToString("d")}\t{formatName}\t{bandName}\t{shelfTag}\t{shelfRow}\t{lost}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }
        
        public int InsertBandAlbum(BandAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBandAlbums (albumName, genreID, dateOfRelease, formatID, bandID, shelfRowID, lost) VALUES (@AlbumName, @GenreId, @DateOfRelease, @FormatId, @BandId, @ShelfRowId, @Lost); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@BandId", albums.BandId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);
                cmd.Parameters.AddWithValue("@Lost", albums.Lost);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandAlbumById(int albumId, string albumName, int genreId, DateTime dateOfRelease, int formatId, int bandId, int shelfRowId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblBandAlbums SET albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, bandID = @bandId, shelfRowId = @shelfRowId, lost = @lost WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@albumName", albumName);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@dateOfRelease", dateOfRelease);
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@bandId", bandId);
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);
                cmd.Parameters.AddWithValue("@lost", lost);

                return cmd.ExecuteNonQuery();
            }
        }

        public int LostBand(int albumId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblBandAlbums SET lost = @lost WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@lost", lost);                

                return cmd.ExecuteNonQuery();
            }
        }

        public int FavouriteBand(int reviewId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblBandReviews SET favourite = @favourite WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                cmd.Parameters.AddWithValue("@favourite", favourite);                

                return cmd.ExecuteNonQuery();
            }
        }
        
        public int DeleteBandAlbumById(int albumId)
        {
            string sqlStr = "DELETE FROM Contents.tblBandAlbums WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetArtistReviewBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 reviewID FROM Contents.tblArtistReviews ORDER BY reviewID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["reviewID"]);
                    }
                }
            }

            return max;

        }

        public List<ArtistReviews> GetAllArtistReviews()
        {
            List<ArtistReviews> albums = new List<ArtistReviews>();
            string sqlStr = "SELECT reviewID, albumName, tierTag, fName, favourite FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistReviews.tierID = tblTier.tierID AND tblArtistReviews.personID = tblAccounts.personID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tNAME:\tRANK:\tFAVOURITE:\n");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        string fName = reader["fName"].ToString();
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        Console.WriteLine($"{reviewId}\t{albumName}\t{tierTag}\t{fName}\t{favourite}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }

        public int GetUsersArtistReviews(int personId)
        {
            string sqlStr = "SELECT reviewID, albumName, tierTag, favourite FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblArtistReviews.personID = @currentPerson AND tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistReviews.tierID = tblTier.tierID AND tblArtistReviews.personID = tblAccounts.personID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@currentPerson", personId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tRANK:\tFAVOURITE\n");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        Console.WriteLine($"{reviewId}\t{albumName}\t{tierTag}\t{favourite}\n");
                        Thread.Sleep(wait);

                    }

                }
            }
            return personId;
        }
        
        public int InsertArtistReview(ArtistReviews reviews)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistReviews (albumID, personID, tierID, favourite) VALUES (@AlbumId, @PersonId, @TierId, @Favourite); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumId", reviews.AlbumId);
                cmd.Parameters.AddWithValue("@PersonId", reviews.PersonId);
                cmd.Parameters.AddWithValue("@TierId", reviews.TierId);
                cmd.Parameters.AddWithValue("@Favourite", reviews.Favourite);
                

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistReviewById(int reviewId, int albumId, int personId, int tierId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblArtistReviews SET albumID = @albumId, tierID = @tierId, personID = @personId, favourite = @favourite WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@tierId", tierId);
                cmd.Parameters.AddWithValue("@personId", personId);
                cmd.Parameters.AddWithValue("@favourite", favourite);


                return cmd.ExecuteNonQuery();
            }
        }
        
        public int DeleteArtistReviewById(int reviewId)
        {
            string sqlStr = "DELETE FROM Contents.tblArtistReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                return cmd.ExecuteNonQuery();
            }

        }


        public int GetBandReviewBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 reviewID FROM Contents.tblBandReviews ORDER BY reviewID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["reviewID"]);
                    }
                }
            }

            return max;

        }

        public List<BandReviews> GetAllBandReviews()
        {
            List<BandReviews> albums = new List<BandReviews>();
            string sqlStr = "SELECT reviewID, albumName, tierTag, fName, favourite FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.tierID = tblTier.tierID AND tblBandReviews.personID = tblAccounts.personID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tRANK:\tFAVOURITE:\n");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        string fName = reader["fName"].ToString();
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        Console.WriteLine($"{reviewId}\t{albumName}\t{tierTag}\t{fName}\t{favourite}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }

        public int GetUsersBandReviews(int personId)
        {
            string sqlStr = "SELECT reviewID, albumName, tierTag, favourite FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblBandReviews.personID = @currentPerson AND tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.tierID = tblTier.tierID AND tblBandReviews.personID = tblAccounts.personID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@currentPerson", personId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\tRANK:\tFAVOURITE\n");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        Console.WriteLine($"{reviewId}\t{albumName}\t{tierTag}\t{favourite}\n");
                        Thread.Sleep(wait);

                    }

                }
            }
            return personId;
        }
        
        public int InsertBandReview(BandReviews reviews)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBandReviews (albumID, personID, tierID, favourite) VALUES (@AlbumId, @PersonId, @TierId, @Favourite); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumId", reviews.AlbumId);
                cmd.Parameters.AddWithValue("@PersonId", reviews.PersonId);
                cmd.Parameters.AddWithValue("@TierId", reviews.TierId);
                cmd.Parameters.AddWithValue("@Favourite", reviews.Favourite);
                

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandReviewById(int reviewId, int albumId, int personId, int tierId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblBandReviews SET albumID = @albumId, tierID = @tierId, personID = @personId, favourite = @favourite WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@tierId", tierId);
                cmd.Parameters.AddWithValue("@personId", personId);
                cmd.Parameters.AddWithValue("@favourite", favourite);


                return cmd.ExecuteNonQuery();
            }
        }
        
        public int DeleteBandReviewById(int reviewId)
        {
            string sqlStr = "DELETE FROM Contents.tblBandReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                return cmd.ExecuteNonQuery();
            }

        }


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


        // Uses the username credential as a parameter for usernames to pass through the scalar variable to return the ID of that person.
        public int FetchAccount(string username)
        {
            int personId = 0; // Had to be initialised outside to remain within scope. The value gets overwritten after the data reader passes what is read through the integer.

            string sqlStr = $"SELECT personID FROM Properties.tblAccounts WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);

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
        public string ScanUsername(string password)
        {
            string username = " ";

            string sqlStr = $"SELECT username FROM Properties.tblAccounts WHERE pw = @password";
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


        // Checks the username and password against themselves, then returns a match for a password in the database.
        public string ScanPassword(string username)
        {
            string password = " ";

            string sqlStr = $"SELECT pw FROM Properties.tblAccounts WHERE username = @username";
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

        
        // Checks the role and username against themselves, then returns a match for a role in the database.
        public int FetchRole(string username)
        {

            int roleId = 0;

            string sqlStr = $"SELECT roleID FROM Properties.tblAccounts WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);

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

        // These two check if there is a match for a person who has left a review under a specified identification number.
        public int FetchAccountFromArtistReviews(int reviewId)
        {

            int personId = 0;

            string sqlStr = $"SELECT personID FROM Contents.tblArtistReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);

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

        public int FetchAccountFromBandReviews(int reviewId)
        {
            int personId = 0;

            string sqlStr = $"SELECT personID FROM Contents.tblBandReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);

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

        public string FetchUsername(string match)
        {
            string username = "";

            string sqlStr = $"SELECT username FROM Properties.tblAccounts WHERE username = @match";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@match", match);

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


        // These four check if an album is already marked as a favourite, or lost.

        public bool FetchFavouriteFromArtistReviews(int reviewId)
        {
            bool favourite = true;

            string sqlStr = $"SELECT favourite FROM Contents.tblArtistReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         favourite = Convert.ToBoolean(reader["favourite"]);
                    }
                }

                return favourite;
            }
        }

        public bool FetchFavouriteFromBandReviews(int reviewId)
        {
            bool favourite = true;

            string sqlStr = $"SELECT favourite FROM Contents.tblBandReviews WHERE reviewID = @reviewId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@reviewId", reviewId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         favourite = Convert.ToBoolean(reader["favourite"]);
                    }
                }

                return favourite;
            }
        }

        public bool FetchLostFromArtistAlbums(int albumId)
        {
            bool lost = true;

            string sqlStr = $"SELECT lost FROM Contents.tblArtistAlbums WHERE albumId = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         lost = Convert.ToBoolean(reader["lost"]);
                    }
                }

                return lost;
            }
        }

        public bool FetchLostFromBandAlbums(int albumId)
        {
            bool lost = true;

            string sqlStr = $"SELECT lost FROM Contents.tblBandAlbums WHERE albumId = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         lost = Convert.ToBoolean(reader["lost"]);
                    }
                }

                return lost;
            }
        }

        
        // Pulls the last identification number from the Formats table to use as the upper boundary.
        public int GetFormatBoundary()
        {
            int max = 0;

            string sqlStr = "SELECT TOP 1 formatID FROM Properties.tblFormat ORDER BY formatID DESC;";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["formatID"]);
                    }
                }
            }

            return max;

        }


        // Pulls data from the Formats table using the reader, and returns it through the class used as an element for the list.
        public List<Formats> GetAllFormats()
        {
            List<Formats> formats = new List<Formats>();
            string sqlStr = "SELECT * FROM Properties.tblFormat";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                Console.WriteLine("ID:\tNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        string formatName = reader["formatName"].ToString();
                        formats.Add(new Formats(formatId, formatName));

                        Console.WriteLine($"{formatId}\t{formatName}\n");
                    }
                }
            }
            return formats;
        }


        // Inserts a new record into the table.
        public int InsertFormat(Formats formats)
        {
            string sqlStr = $"INSERT INTO Properties.tblFormat (formatName) VALUES (@FormatName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@FormatName", formats.FormatName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        
        // Updates through the formats identification number. 
        public int UpdateFormatById(int formatId, string formatName)
        {
            string sqlStr = $"UPDATE Properties.tblFormat SET formatName = @formatName WHERE formatID = @formatId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@formatName", formatName);
                return cmd.ExecuteNonQuery();
            }
        }


        // Deletes the format through the identification number.
        public int DeleteFormatById(int formatId)
        {
            string sqlStr = "DELETE FROM Properties.tblFormat WHERE formatID = @formatId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@formatId", formatId);
                return cmd.ExecuteNonQuery();
            }

        }


        /* 
        
        All functions follow the the format modifiers functions example; 
        All these functions are used as a bridge between the database and the application 
        to send signals and recieve signals from. The comments left for these functions apply to all else regarding functions here. 
        
        */


        // Artist Queries

        public void GetArtistReleaseDate()

        {
            string sqlStr = "SELECT albumName, dateOfRelease FROM Contents.tblArtistAlbums; ";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetAToJArtists()
        {
            string sqlStr = "SELECT tblArtists.artistName, tblArtistAlbums.albumName, tblGenres.genreName FROM Contents.tblArtists, Contents.tblArtistAlbums, Contents.tblGenres WHERE tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.genreID = tblGenres.genreID AND tblArtists.artistName LIKE '[A-J]%' ORDER BY tblArtists.artistName asc";
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

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetArtistsEarly2000sMusic()
        {
            string sqlStr = "SELECT tblArtistAlbums.albumName, tblArtists.artistName, tblArtistAlbums.dateOfRelease FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.dateOfRelease BETWEEN '20000101' AND '20051231'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        string artistName = reader["artistName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumName}, {artistName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTotalArtistGenres()
        {
            string sqlStr = "SELECT COUNT(tblArtistAlbums.genreID) as Count, tblGenres.genreName FROM Contents.tblArtistAlbums, Contents.tblGenres WHERE tblArtistAlbums.genreID = tblGenres.genreID GROUP BY tblGenres.genreName ";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string count = reader["Count"].ToString();
                        string genreName = reader["genreName"].ToString();

                        Console.WriteLine($"{count}, {genreName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetArtistsAsAWhole()
        {
            string sqlStr = "SELECT bandName as 'All Artists' FROM Contents.tblBands UNION SELECT artistName FROM Contents.tblArtists ORDER BY 'All Artists' asc";
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

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTotalAlbumsPublishedByArtists()
        {
            string sqlStr = "SELECT COUNT(tblArtistAlbums.albumID) as Count, tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtistAlbums.artistID = tblArtists.artistID GROUP BY tblArtists.artistName ORDER BY tblArtists.artistName;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["Count"]);
                        string artistName = reader ["artistName"].ToString();

                        Console.WriteLine($"{albumId}, {artistName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTotalPublishingsOfAllArtistsPerYear()
        {
            string sqlStr = "SELECT COUNT(tblArtistAlbums.albumID) as Count, YEAR(tblArtistAlbums.dateOfRelease) as 'dateOfRelease' FROM Contents.tblArtistAlbums GROUP BY YEAR(tblArtistAlbums.dateOfRelease) ORDER BY 'dateOfRelease'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["Count"]);
                        int dateOfRelease = Convert.ToInt32(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumId}, {dateOfRelease}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }


        // Band Queries

        public void GetBandReleaseDate()

        {
            string sqlStr = "SELECT albumName, dateOfRelease FROM Contents.tblBandAlbums; ";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetAToJBands()
        {
            string sqlStr = "SELECT tblBands.bandName, tblBandAlbums.albumName, tblGenres.genreName FROM Contents.tblBands, Contents.tblBandAlbums, Contents.tblGenres WHERE tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.genreID = tblGenres.genreID AND tblBands.bandName LIKE '[A-J]%' ORDER BY tblBands.bandName asc";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string bandName = reader["bandName"].ToString();
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();

                        Console.WriteLine($"{bandName}, {albumName}, {genreName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetBandsEarly2000sMusic()
        {
            string sqlStr = "SELECT tblBandAlbums.albumName, tblBands.bandName, tblBandAlbums.dateOfRelease FROM Contents.tblBandAlbums, Contents.tblBands WHERE tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.dateOfRelease BETWEEN '20000101' AND '20051231'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        string bandName = reader["bandName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumName}, {bandName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTotalBandGenres()
        {
            string sqlStr = "SELECT COUNT(tblBandAlbums.genreID) as Count, tblGenres.genreName FROM Contents.tblBandAlbums, Contents.tblGenres WHERE tblBandAlbums.genreID = tblGenres.genreID GROUP BY tblGenres.genreName ";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string count = reader["Count"].ToString();
                        string genreName = reader["genreName"].ToString();

                        Console.WriteLine($"{count}, {genreName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetBandsAsAWhole()
        {
            string sqlStr = "SELECT bandName as 'All Bands' FROM Contents.tblBands UNION SELECT artistName FROM Contents.tblArtists ORDER BY 'All Artists' asc";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string allBands = reader["All Artists"].ToString();

                        Console.WriteLine($"{allBands}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }

        public void GetTotalAlbumsPublishedByBands()
        {
            string sqlStr = "SELECT COUNT(tblBandAlbums.albumID) as 'Count', tblBands.bandName FROM Contents.tblBandAlbums, Contents.tblBands WHERE tblBandAlbums.bandID = tblBands.bandID GROUP BY tblBands.bandName ORDER BY tblBands.bandName;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["Count"]);
                        string bandName = reader ["bandName"].ToString();

                        Console.WriteLine($"{albumId}, {bandName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTotalPublishingsOfAllBandsPerYear()
        {
            string sqlStr = "SELECT COUNT(tblBandAlbums.albumID) as 'Count', YEAR(tblBandAlbums.dateOfRelease) as 'dateOfRelease' FROM Contents.tblBandAlbums GROUP BY YEAR(tblBandAlbums.dateOfRelease) ORDER BY 'dateOfRelease'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["Count"]);
                        int dateOfRelease = Convert.ToInt32(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumId}, {dateOfRelease}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }


        // Review Queries

        public void GetHighRankingArtistAlbums()
        {
            string sqlStr = "SELECT tblArtistAlbums.albumName, tblTier.tierTag FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Properties.tblTier WHERE tblTier.tierTag = 'S' AND tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistReviews.tierID = tblTier.tierID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);

                        Console.WriteLine($"{albumName}, {tierTag}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetHighRankingBandAlbums()
        {
            string sqlStr = "SELECT tblBandAlbums.albumName, tblTier.tierTag FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblTier WHERE tblTier.tierTag = 'S' AND tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.tierID = tblTier.tierID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);

                        Console.WriteLine($"{albumName}, {tierTag}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTopThreeFavouriteArtistAlbums()
        {
            string sqlStr = "SELECT TOP 3 COUNT(tblArtistReviews.favourite) as 'favourite', tblArtistAlbums.albumName FROM Contents.tblArtistReviews, Contents.tblArtistAlbums WHERE tblArtistReviews.albumID = tblArtistAlbums.albumID AND favourite = 'true' GROUP BY tblArtistAlbums.albumName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool favourite = Convert.ToBoolean(reader["favourite"]);
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{favourite}, {albumName}\n");
                        Thread.Sleep(wait);
                    }
                }

                Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

            }
        }

        public void GetTopThreeFavouriteBandAlbums()
        {
            string sqlStr = "SELECT TOP 3 COUNT(tblBandReviews.favourite) as 'favourite', tblBandAlbums.albumName FROM Contents.tblBandReviews, Contents.tblBandAlbums WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND favourite = 'true' GROUP BY tblBandAlbums.albumName";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool favourite = Convert.ToBoolean(reader["favourite"]);
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{favourite}, {albumName}\n");
                        Thread.Sleep(wait);
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }

        // Shelf and Row Queries

        public void GetTotalOfRowOccupancyPerShelf()
        {
            string sqlStr = "SELECT COUNT(tblRow.shelfRowID) as 'shelfRowID', tblShelf.shelfTag FROM Properties.tblRow, Properties.tblShelf WHERE tblRow.shelfTagID = tblShelf.shelfTagID GROUP BY tblShelf.shelfTag";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);

                        Console.WriteLine($"{shelfTag}, {shelfRowId}\n");
                        Thread.Sleep(wait);
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        // Search options

        public void SearchGenres(string search)
        {

            string sqlStr = $"SELECT genreName FROM Contents.tblGenres WHERE genreName LIKE '%' + @search + '%'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nCATEGORY:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["genreName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchArtists(string search)
        {

            string sqlStr = $"SELECT artistName FROM Contents.tblArtists WHERE artistName LIKE '%' + @search + '%'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["artistName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchBands(string search)
        {

            string sqlStr = $"SELECT bandName FROM Contents.tblBands WHERE bandName LIKE '%' + @search + '%'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["bandName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchArtistReviews(string search)
        {

            string sqlStr = $"SELECT tblTier.tierTag, tblArtistAlbums.albumName FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Properties.tblTier WHERE tblTier.tierTag LIKE '%' + @search + '%' AND tblArtistReviews.tierID = tblTier.tierID AND tblArtistReviews.albumID = tblArtistAlbums.albumID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nRANK:\tNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["tierTag"].ToString();
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{search}\t{albumName}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchBandReviews(string search)
        {

            string sqlStr = $"SELECT tblTier.tierTag, tblBandAlbums.albumName FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblTier WHERE tblTier.tierTag LIKE '%' + @search + '%' AND tblBandReviews.tierID = tblTier.tierID AND tblBandReviews.albumID = tblBandAlbums.albumID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nRANK:\tNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["tierTag"].ToString();
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{search}\t{albumName}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchArtistAlbums(string search)
        {

            string sqlStr = $"SELECT tblArtists.artistName, tblArtistAlbums.albumName FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtistAlbums.albumName LIKE '%' + @search + '%' AND tblArtistAlbums.artistID = tblArtists.artistID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nNAME:\tNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["artistName"].ToString();
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{search}\t{albumName}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        public void SearchBandAlbums(string search)
        {

            string sqlStr = $"SELECT tblBands.bandName, tblBandAlbums.albumName FROM Contents.tblBandAlbums, Contents.tblBands WHERE tblBandAlbums.bandName LIKE '%' + @search + '%' AND tblBandAlbums.bandID = tblBands.bandID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("\nNAME:\tNAME:\n");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["bandName"].ToString();
                        string albumName = reader["albumName"].ToString();

                        Console.WriteLine($"{search}\t{albumName}\n");
                    }

                    Console.WriteLine("\n\t[*]  Return to homepage - Press E + Enter\n");

                }
            }
        }


        // Closes the connection; Evaluates whether to based on if the connection is null, or if the state says its open. If the first is returned as false, the second isn't evaluated.
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
