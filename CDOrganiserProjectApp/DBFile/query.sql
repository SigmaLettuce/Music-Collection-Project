-- This is a query that is used to test the functionality of a query.

SELECT albumName, genreName, dateOfRelease, formatName, bandName, roomName, shelfTag, shelfRow 
FROM Contents.tblBandAlbums, Properties.tblFormat, Contents.tblBands, Properties.tblStorageRoom 
WHERE tblBandAlbums.formatID = tblFormat.formatID 
AND tblBandAlbums.bandID = tblBands.bandID 
AND tblBandAlbums.roomID = tblStorageRoom.roomID

INSERT INTO Contents.tblAlbums (albumName, genreName, dateOfRelease, formatID, artistID, roomID, shelfTag, shelfRow) 
VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @ArtistName, @RoomName, @ShelfTag, @ShelfRow); 


SELECT SCOPE_IDENTITY();