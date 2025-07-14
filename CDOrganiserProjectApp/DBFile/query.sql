
SELECT tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblArtists WHERE tblArtists.artistName = @search AND tblArtistAlbums.artistID = tblArtists.artistID