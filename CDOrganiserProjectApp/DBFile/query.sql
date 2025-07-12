-- This is a query that is used to test the functionality of a query.

SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, shelfRow, username, tierTag, favourite, lost 
FROM Contents.tblGenres, Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblRow, Properties.tblAccounts, Properties.tblTier 
WHERE tblArtistAlbums.genreID = tblGenres.genreID 
AND tblArtistAlbums.formatID = tblFormat.formatID 
AND tblArtistAlbums.artistID = tblArtists.artistID 
AND tblArtistAlbums.shelfRowID = tblRow.shelfRowID 
AND tblArtistAlbums.personID = 2 
AND tblArtistAlbums.tierID = tblTier.tierID