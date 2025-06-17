-- This is a query that is used to test the functionality of a query.

SELECT albumID, albumName, genreName, dateOfRelease, formatName, artistName, roomName, shelfTag, shelfRow, lost 
FROM Contents.tblArtistAlbums, Properties.tblFormat, Contents.tblArtists, Properties.tblStorageRoom
WHERE tblArtistAlbums.formatID = tblFormat.formatID
AND tblArtistAlbums.artistID = tblArtists.artistID
AND tblArtistAlbums.roomID = tblStorageRoom.roomID
