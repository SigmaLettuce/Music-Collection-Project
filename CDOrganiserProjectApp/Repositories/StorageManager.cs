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

        /*                      *\  
         
            [STORAGE MANAGER]
         
        \*                      */


        // A generically shared integer for delays.
        int wait = 100; 

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
            string sqlStr = "DELETE FROM Contents.tblArtists WHERE artistID = @artistId";
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
                Console.WriteLine("ID:  ROOM:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roomId = Convert.ToInt32(reader["roomID"]);
                        string roomName = reader["roomName"].ToString();

                        rooms.Add(new Rooms(roomId, roomName));

                        Console.WriteLine($"{roomId}, {roomName}\n");
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


        public List<Shelves> GetAllShelves()
        {
            List<Shelves> shelves = new List<Shelves>();
            string sqlStr = "SELECT tblShelf.shelfTagID, tblShelf.shelfTag, tblShelf.roomID, tblStorageRoom.roomName FROM Properties.tblShelf, Properties.tblStorageRoom WHERE tblShelf.roomID = tblStorageRoom.roomID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:  TAG:   ROOM:");

                    while (reader.Read())
                    {
                        int shelfTagId = Convert.ToInt32(reader["shelfTagID"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);
                        int roomId = Convert.ToInt32(reader["roomID"]);

                        string roomName = reader["roomName"].ToString();

                        shelves.Add(new Shelves(shelfTagId, shelfTag, roomId));

                        Console.WriteLine($"{shelfTagId}, {shelfTag}, {roomName}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return shelves;
        }

        public int InsertShelf(Shelves shelves)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Properties.tblShelf (shelfTag, roomName) VALUES (@shelfTag, @roomName); SELECT SCOPE_IDENTITY();", conn))
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


        public List<Rows> GetAllRows()
        {
            List<Rows> rows = new List<Rows>();
            string sqlStr = "SELECT tblShelf.shelfTagID, tblShelf.shelfTag, tblShelf.roomID, tblStorageRoom.roomName FROM Properties.tblShelf, Properties.tblStorageRoom WHERE tblShelf.roomID = tblStorageRoom.roomID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:  ROW:   TAG:");

                    while (reader.Read())
                    {
                        int shelfRowId = Convert.ToInt32(reader["shelfRowID"]);
                        int shelfRow = Convert.ToInt32(reader["shelfRow"]);
                        int shelfTagId = Convert.ToInt32(reader["shelfTagID"]);

                        char shelfTag = Convert.ToChar(reader["roomName"]);

                        rows.Add(new Rows(shelfRowId, shelfTag, shelfTagId));

                        Console.WriteLine($"{shelfRowId}, {shelfRow}, {shelfTag}\n");
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


        public List<ArtistAlbums> GetAllArtistAlbums()
        {
            List<ArtistAlbums> albums = new List<ArtistAlbums>();
            string sqlStr = "SELECT albumID, albumName, tblArtistAlbums.genreID, genreName, dateOfRelease, tblArtistAlbums.formatID, formatName, tblArtistAlbums.artistID, artistName, tblArtistAlbums.shelfRowID, shelfRow, lost FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblRow WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.formatID = tblFormat.formatID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.shelfRowID = tblRow.shelfRowID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:NAME:CATEGORY:RELEASE DATE:FORMAT:ARTIST:ROW:FAVOURITE:");

                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string albumName = reader["albumName"].ToString();
                        int genreId = Convert.ToInt32(reader["genreID"]);
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

        public int UpdateArtistAlbumById(int albumId, string albumName, int genreId, DateTime dateOfRelease, int formatId, int artistId, int shelfRowId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET (albumID = @albumId, albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, artistID = @artistId, shelfRowId = @shelfRowId, lost = @lost) WHERE albumID = @albumId";
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
        
        public int LostArtist(int albumId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET (lost = @lost) WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@favourite", favourite);                

                return cmd.ExecuteNonQuery();
            }
        }

        public int FavouriteArtist(int reviewId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblArtistReviews SET (favourite = @favourite) WHERE reviewID = @reviewId";
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


        public List<BandAlbums> GetAllBandAlbums()
        {
            List<BandAlbums> albums = new List<BandAlbums>();
            string sqlStr = "SELECT albumID, albumName, tblBandAlbums.genreID, genreName, dateOfRelease, tblBandAlbums.formatID, formatName, tblBandAlbums.bandID, bandName, tblBandAlbums.shelfRowID, shelfRow, lost FROM Contents.tblGenres, Contents.tblBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblRow WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.formatID = tblFormat.formatID AND tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.shelfRowId = tblRow.shelfRowId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID:NAME:CATEGORY:RELEASE DATE:FORMAT:ARTIST:ROW:FAVOURITE:");

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



                        Console.WriteLine($"{albumId}, {albumName}, {genreName}, {dateOfRelease.ToString("d")}, {formatName}, {bandName}, {shelfRow}, {lost}\n");
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
                cmd.Parameters.AddWithValue("@AlbumId", albums.AlbumId);
                cmd.Parameters.AddWithValue("@AlbumName", albums.AlbumName);
                cmd.Parameters.AddWithValue("@GenreId", albums.GenreId);
                cmd.Parameters.AddWithValue("@DateOfRelease", albums.DateOfRelease);
                cmd.Parameters.AddWithValue("@FormatId", albums.FormatId);
                cmd.Parameters.AddWithValue("@BandId", albums.BandId);
                cmd.Parameters.AddWithValue("@ShelfRowId", albums.ShelfRowId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateBandAlbumById(int albumId, string albumName, int genreId, DateTime dateOfRelease, int formatId, int bandId, int shelfRowId, bool lost)
        {
            string sqlStr = $"UPDATE Contents.tblBandAlbums SET (albumID = @albumId, albumName = @albumName, genreID = @genreId, dateOfRelease = @dateOfRelease, formatID = @formatId, bandID = @bandId, shelfRowId = @shelfRowId, lost = @lost) WHERE albumID = @albumId";
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

        public int LostBand(int albumId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblBandAlbums SET (lost = @lost) WHERE albumID = @albumId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@favourite", favourite);                

                return cmd.ExecuteNonQuery();
            }
        }

        public int FavouriteBand(int reviewId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblBandReviews SET (favourite = @favourite) WHERE reviewID = @reviewId";
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


        public List<ArtistReviews> GetAllArtistReviews(int personId)
        {
            List<ArtistReviews> albums = new List<ArtistReviews>();
            string sqlStr = "SELECT reviewID, tblArtistReviews.albumID, albumName, tblArtistReviews.personID, fName, tblArtistReviews.tierID, tierTag, favourite FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistReviews.personID = @currentPerson AND tblArtistReviews.tierID = tblTier.tierID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@currentPerson", personId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID: NAME: RANK: FAVOURITE:");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        int tierId = Convert.ToInt32(reader["tierID"]);
                        bool favourite = Convert.ToBoolean(reader["favourite"]);

                        string albumName = reader["albumName"].ToString();
                        string fName = reader["fName"].ToString();
                        char tierTag = Convert.ToChar(reader["tierTag"]);

                        albums.Add(new ArtistReviews(reviewId, albumId, personId, tierId, favourite));



                        Console.WriteLine($"{reviewId}, {albumName}, {tierTag}, {favourite}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
            return albums;
        }
        
        public int InsertArtistReview(ArtistReviews albums)
        {
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO Contents.ArtistReviews (albumID, tierID) VALUES (@AlbumId, @TierId); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@AlbumId", albums.AlbumId);
                cmd.Parameters.AddWithValue("@TierId", albums.TierId);
                

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateArtistReviewById(int reviewId, int albumId, int tierId, int personId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblArtistAlbums SET (reviewID = @reviewId, albumID = @albumId, tierID = @tierId, personID = @personId, favourite = @favourite) WHERE reviewID = @reviewId";
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


        public List<BandReviews> GetAllBandReviews(int personId)
        {
            List<BandReviews> albums = new List<BandReviews>();
            string sqlStr = "SELECT reviewID, albumName, fName, tierTag, favourite FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.personID = @currentPerson AND tblBandReviews.tierID = tblTier.tierID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@currentPerson", personId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("ID: NAME: CATEGORY: FIRST NAME: RANK: FAVOURITE:");

                    while (reader.Read())
                    {
                        int reviewId = Convert.ToInt32(reader["reviewID"]);
                        int albumId = Convert.ToInt32(reader["albumID"]);
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

        public int UpdateBandReviewById(int reviewId, int albumId, int tierId, int personId, bool favourite)
        {
            string sqlStr = $"UPDATE Contents.tblBandReviews SET (reviewID = @reviewId, albumID = @albumId, tierID = @tierId, personID = @personId, favourite = @favourite) WHERE reviewID = @reviewId";
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


        public List<Accounts> GetAllAccounts;

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

        public int DeleteAccountById(int personId)
        {
            string sqlStr = "DELETE FROM Properties.tblAccounts WHERE personID = @personId";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@personId", personId);
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
                Console.WriteLine("ID:  NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int formatId = Convert.ToInt32(reader["formatID"]);
                        string formatName = reader["formatName"].ToString();
                        formats.Add(new Formats(formatId, formatName));

                        Console.WriteLine($"{formatId}, {formatName}\n");
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
                        string artistName = reader["artistName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{artistName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }

        public void GetAToJArtists()
        {
            string sqlStr = "SELECT tblArtists.artistName, tblArtistAlbums.albumName, tblAlbums.genreName FROM Contents.tblArtists, Contents.tblAlbums WHERE tblArtistAlbums.artistID = tblArtists.artistID AND tblArtists.artistName LIKE '[A-J]%' ORDER BY tblArtists.artistName, tblAlbums.albumName, tblAlbums.genreName;";
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

        public void GetArtistsEarly2000sMusic()
        {
            string sqlStr = "SELECT tblArtistAlbums.albumName, tblGenres.genreName, tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblGenres, Contents.tblArtists WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.dateOfRelease BETWEEN '20000101' AND '20051231'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();
                        string artistName = reader["artistName"].ToString();

                        Console.WriteLine($"{albumName}, {genreName}, {artistName}\n");
                        Thread.Sleep(wait);
                    }
                }
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
            }
        }

        public void GetTotalAlbumsPublishedByArtists()
        {
            string sqlStr = "SELECT COUNT(tblArtistAlbums.albumID), tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtistAlbums.artistID = tblArtists.artistID GROUP BY tblArtists.artistName ORDER BY tblArtists.artistName;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string artistName = reader ["artistName"].ToString();

                        Console.WriteLine($"{albumId}, {artistName}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }

        public void GetTotalPublishingsOfAllArtistsPerYear()
        {
            string sqlStr = "SELECT COUNT(tblArtistAlbums.albumID), YEAR(tblArtistAlbums.dateOfRelease) as 'dateOfRelease' FROM Contents.tblArtistAlbums GROUP BY YEAR(tblArtistAlbums.dateOfRelease) ORDER BY 'dateOfRelease'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumId}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }
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
                        string bandName = reader["bandName"].ToString();
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{bandName}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }

        public void GetAToJBands()
        {
            string sqlStr = "SELECT tblBands.bandName, tblBandAlbums.albumName, tblAlbums.genreName FROM Contents.tblBands, Contents.tblAlbums WHERE tblBandAlbums.bandID = tblBands.bandID AND tblBands.bandName LIKE '[A-J]%' ORDER BY tblBands.bandName asc, tblAlbums.albumName, tblAlbums.genreName;";
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
            }
        }

        public void GetBandsEarly2000sMusic()
        {
            string sqlStr = "SELECT tblBandAlbums.albumName, tblGenres.genreName, tblBands.bandName FROM Contents.tblBandAlbums, Contents.tblGenres, Contents.tblBands WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.dateOfRelease BETWEEN '20000101' AND '20051231'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string albumName = reader["albumName"].ToString();
                        string genreName = reader["genreName"].ToString();
                        string bandName = reader["bandName"].ToString();

                        Console.WriteLine($"{albumName}, {genreName}, {bandName}\n");
                        Thread.Sleep(wait);
                    }
                }
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
            string sqlStr = "SELECT COUNT(tblBandAlbums.albumID), tblBands.bandName FROM Contents.tblBandAlbums, Contents.tblBands WHERE tblBandAlbums.bandID = tblBands.bandID GROUP BY tblBands.bandName ORDER BY tblBands.bandName;";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        string bandName = reader ["bandName"].ToString();

                        Console.WriteLine($"{albumId}, {bandName}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }

        public void GetTotalPublishingsOfAllBandsPerYear()
        {
            string sqlStr = "SELECT COUNT(tblBandAlbums.albumID), YEAR(tblBandAlbums.dateOfRelease) as 'dateOfRelease' FROM Contents.tblBandAlbums GROUP BY YEAR(tblBandAlbums.dateOfRelease) ORDER BY 'dateOfRelease'";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int albumId = Convert.ToInt32(reader["albumID"]);
                        DateTime dateOfRelease = Convert.ToDateTime(reader["dateOfRelease"]);

                        Console.WriteLine($"{albumId}, {dateOfRelease.ToString("d")}\n");
                        Thread.Sleep(wait);
                    }
                }
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
            }
        }

        public void GetTopThreeFavouriteArtistAlbums()
        {
            string sqlStr = "SELECT TOP 3 COUNT(tblArtistReviews.favourite), tblArtistAlbums.albumName FROM Contents.tblArtistReviews, Contents.tblArtistAlbums WHERE tblArtistReviews.albumID = tblArtistAlbums.albumID AND favourite = 'true' GROUP BY tblArtistAlbums.albumName";
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
            }
        }

        public void GetTopThreeFavouriteBandAlbums()
        {
            string sqlStr = "SELECT TOP 3 COUNT(tblBandReviews.favourite), tblBandAlbums.albumName FROM Contents.tblBandReviews, Contents.tblBandAlbums WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND favourite = 'true' GROUP BY tblBandAlbums.albumName";
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
            }
        }

        // Shelf and Row Queries

        public void GetTotalOfRowOccupancyPerShelf()
        {
            string sqlStr = "SELECT COUNT(tblRow.shelfRowID), tblShelf.shelfTag FROM Properties.tblRow, Properties.tblShelf WHERE tblRow.shelfTagID = tblShelf.shelfTagID GROUP BY tblShelf.shelfTag";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int shelfRowID = Convert.ToInt32(reader["shelfRowID"]);
                        char shelfTag = Convert.ToChar(reader["shelfTag"]);

                        Console.WriteLine($"{shelfTag}, {shelfRowID}\n");
                        Thread.Sleep(wait);
                    }
                }
            }
        }


        // Search options

        public string SearchGenres(string search)
        {

            string sqlStr = $"SELECT genreName FROM Contents.tblGenres WHERE genreName = @search";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("CATEGORY:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["genreName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchArtists(string search)
        {

            string sqlStr = $"SELECT artistName FROM Contents.tblArtistAlbums WHERE artistName = @search";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["artistName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchBands(string search)
        {

            string sqlStr = $"SELECT bandName FROM Contents.tblArtistAlbums WHERE bandName = @search";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["bandName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchBandReviews(string search)
        {

            string sqlStr = $"SELECT tblTier.tierTag FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Contents.tblArtists, Properties.tblTier WHERE tblArtists.artistName = @search AND tblArtistReviews.tierID = tblTier.tierID AND tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistAlbums.artistID = tblArtists.artistID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["tierTag"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchArtistReviews(string search)
        {

            string sqlStr = $"SELECT tblTier.tierTag FROM Contents.tblBandReviews, Contents.tblBandAlbums, Contents.tblBands, Properties.tblTier WHERE tblBands.bandName = @search AND tblBandReviews.tierID = tblTier.tierID  AND tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandAlbums.bandID = tblBands.bandID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["tierTag"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchArtistAlbums(string search)
        {

            string sqlStr = $"SELECT tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtists.artistName = @search AND tblArtistAlbums.artistID = tblArtists.artistID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["artistName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
                }
            }
        }


        public string SearchBandAlbums(string search)
        {

            string sqlStr = $"SELECT tblBands.bandName FROM Contents.tblBandAlbums, Contents.tblBandsWHERE tblBands.bandName = @search AND tblBandAlbums.bandID = tblBands.bandID";
            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                cmd.Parameters.AddWithValue("@search", search);
                Console.WriteLine("NAME:");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        search = reader["bandName"].ToString();

                        Console.WriteLine($"{search}\n");
                    }

                    return search;
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
