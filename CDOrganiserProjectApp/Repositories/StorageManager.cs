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
            string sqlStr = "DELETE FROM Contents.tblArtists WHERE artistId = @artistId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@artistId", artistId);
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
            string sqlStr = "DELETE FROM Properties.tblStorageRoom WHERE roomId = @roomId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@roomId", roomId);
                return cmd.ExecuteNonQuery();
            }

        }


        public List<Shelves> GetAllShelves()
        {
            List<Shelves> shelves = new List<Shelves>();
            string sqlStr = "SELECT * FROM Properties.tblShelf";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID: TAG: ROOM:");

                    while (reader.Read())
                    {
                        int shelfTagId = Convert.ToInt32(reader["shelfTagID"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);
                        int roomId = Convert.ToInt32(reader["roomName"]);
                        shelves.Add(new Shelves(shelfTagId, shelfTag, roomId));
                    }
                }
            }
            return shelves;
        }

        public int InsertShelf(Shelves shelves)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Properties.tblShelf (roomName) VALUES (@roomName); SELECT SCOPE_IDENTITY();", conn))
            {
        
                cmd.Parameters.AddWithValue("@roomId", shelves.RoomId);
                
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateShelfRoomById(int shelfTagId, int roomId)
        {
            string sqlStr = $"UPDATE Properties.tblShelf SET roomId = @roomId WHERE shelfTagID = @shelfTagId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfTagId", shelfTagId);
                cmd.Parameters.AddWithValue("@roomId", roomId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteShelfRoomById(int shelfTagId)
        {
            string sqlStr = "DELETE FROM Properties.tblShelf WHERE shelfTagID = @shelfTagId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@shelfTagId", shelfTagId);
                return cmd.ExecuteNonQuery();
            }

        }


        public List<ArtistAlbums> GetAllArtistAlbums()
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, lost FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblRow WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.shelfRowId = tblRow.shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:  NAME:\tCATEGORY:\tRELEASE DATE:\tFORMAT:\tARTIST\tROW:\tFAVOURITE:\t");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        int genreId = Convert.ToChar(reader["genreID"]);
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        int artistId = Convert.ToInt32(reader["artistID"]);
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        string genreName = reader["genreName"].ToString();
                        string formatName = reader["formatName"].ToString();
                        string artistName = reader["artistName"].ToString();
                        string shelfRow = reader["shelfRow"].ToString();                      

                        albums.Add(new ArtistAlbums(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId, lost));

                        
            
                        Console.WriteLine($"{albumId}, {albumName}, {genreName}, \t\t\t\t{dateOfRelease.ToString("d")}, {formatName}, {artistName}, {shelfRow}, {lost}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }

        public int InsertArtistAlbum(ArtistAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistAlbums (albumName, genreID, dateOfRelease, formatID, artistID, shelfRowID) VALUES (@albumName, @GenreId, @DateOfRelease, @FormatId, @ArtistName, @ShelfRowId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@ArtistId", albums.ArtistId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistAlbum(string albumName, int genreId, DateTime dateOfRelease, int formatId, int artistId, int shelfRowId)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET (albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, artistID = @artistId, shelfRowId = @shelfRowId) WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumName", albumName);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@dateOfRelease", dateOfRelease);
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);

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


        public List<BandAlbums> GetAllBandAlbums()
        {
            List<BandAlbums> albums = new List<BandAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, bandName, shelfRow, lost FROM Contents.tblGenres, Contents.tblBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblRow WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.formatID = tblFormat.formatID AND tblBandAlbums.bandID = tblArtists.bandID AND tblBandAlbums.shelfRowId = tblRow.shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:  NAME:\tCATEGORY:\tRELEASE DATE:\tFORMAT:\tARTIST\tROW:\tFAVOURITE:\t");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        int genreId = Convert.ToChar(reader["genreID"]);
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        int bandId = Convert.ToInt32(reader["bandId"]);
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        string genreName = reader["genreName"].ToString();
                        string formatName = reader["formatName"].ToString();
                        string bandName = reader["bandName"].ToString();
                        string shelfRow = reader["shelfRow"].ToString();

                        albums.Add(new BandAlbums(albumId, albumName, genreId, dateOfRelease, formatId, bandId, shelfRowId, lost));



                        Console.WriteLine($"{albumId}, {albumName}, {genreName}, \t\t\t\t{dateOfRelease.ToString("d")}, {formatName}, {bandName}, {shelfRow}, {lost}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }
        
        public int InsertBandAlbum(BandAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBandAlbums (albumName, genreID, dateOfRelease, formatID, bandID, shelfRowID) VALUES (@AlbumId, @GenreId, @DateOfRelease, @FormatId, @BandId, @ShelfRowId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@BandId", albums.BandId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandAlbum(string albumName, int genreId, DateTime dateOfRelease, int formatId, int bandId, int shelfRowId)
        {
            string sqlStr = $"UPDATE Contents.tblBandAlbums SET (albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, bandID = @bandId, shelfRowId = @shelfRowId) WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumName", albumName);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@dateOfRelease", dateOfRelease);
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@bandId", bandId);
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);

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


        public List<ArtistReviews> GetAllArtistReviews()
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, lost FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblRow WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.shelfRowId = tblRow.shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:  NAME:\tCATEGORY:\tRELEASE DATE:\tFORMAT:\tARTIST\tROW:\tFAVOURITE:\t");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        int genreId = Convert.ToChar(reader["genreID"]);
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        int artistId = Convert.ToInt32(reader["artistID"]);
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        bool lost = Convert.ToBoolean(reader["lost"]);

                        string genreName = reader["genreName"].ToString();
                        string formatName = reader["formatName"].ToString();
                        string artistName = reader["artistName"].ToString();
                        string shelfRow = reader["shelfRow"].ToString();                      

                        albums.Add(new ArtistAlbums(albumId, albumName, genreId, dateOfRelease, formatId, artistId, shelfRowId, lost));

                        
            
                        Console.WriteLine($"{albumId}, {albumName}, {genreName}, \t\t\t\t{dateOfRelease.ToString("d")}, {formatName}, {artistName}, {shelfRow}, {lost}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }

        public int InsertArtistReview(ArtistAlbums albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblArtistReviews (albumName, genreID, dateOfRelease, formatID, artistID, shelfRowID) VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @ArtistName, @ShelfRowId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@ArtistId", albums.ArtistId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistReview(string albumName, int genreId, DateTime dateOfRelease, int formatId, int artistId, int shelfRowId)
        {
            string sqlStr = $"UPDATE Contents.tblAlbums SET (albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, artistID = @artistId, shelfRowId = @shelfRowId) WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumName", albumName);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@dateOfRelease", dateOfRelease);
                cmd.Parameters.AddWithValue("@formatId", formatId);
                cmd.Parameters.AddWithValue("@artistId", artistId);
                cmd.Parameters.AddWithValue("@shelfRowId", shelfRowId);

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteArtistReviewById(int albumId)
        {
            string sqlStr = "DELETE FROM Contents.tblArtistAlbums WHERE albumId = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                return cmd.ExecuteNonQuery();
            }

        }


        public List<BandReviews> GetAllBandReviews()
        {
            List<BandReviews> albums = new List<BandReviews>();
            string sqlStr = "SELECT reviewID, albumName, fName, tierTag, favourite FROM Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.personID = tblAccounts.personID AND tblBandReviews.tierID = tblTier.tierID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:\tNAME:\t\tCATEGORY:\t\tFIRST NAME:\tRANK:\tFAVOURITE:\t");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        int personId = Convert.ToInt32(reader["personID"]);
                        int tierId = Convert.ToInt32(reader["tierID"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        string albumName = reader["albumName"].ToString();
                        string fName = reader["fName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);

                        albums.Add(new BandReviews(albumId, albumId, personId, tierTag, favourite));



                        Console.WriteLine($"{reviewId}, {albumName}, {tierTag}, {favourite}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }
        
        public int InsertBandReview(BandReviews albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.tblBandReviews (albumID, tierID) VALUES (@AlbumId, @TierId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumId", albums.AlbumId);
                cmd.Parameters.AddWithValue("@TierId", albums.TierId);
                

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandReview(int reviewId, int albumId, int tierId, int personId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblAlbums SET (albumID = @albumId, tierID = @tierId, personID = @personId, favourite = @favourite) WHERE reviewID = @reviewId";
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


        // Associates all the listed with the insert; basically adds a new  
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


        // Pulls data from the Formats table using the reader, and returns a list.
        public int DeleteFormatById(int formatId)
        {
            string sqlStr = "DELETE FROM Properties.tblFormat WHERE formatID = @formatId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@formatId", formatId);
                return cmd.ExecuteNonQuery();
            }

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
