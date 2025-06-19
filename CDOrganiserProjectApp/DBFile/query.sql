-- This is a query that is used to test the functionality of a query.


INSERT INTO Contents.tblAlbums (albumName, genreName, dateOfRelease, formatID, artistID, roomID, shelfTag, shelfRow) 
VALUES (@AlbumName, @GenreName, @DateOfRelease, @FormatName, @ArtistName, @RoomName, @ShelfTag, @ShelfRow); 


SELECT SCOPE_IDENTITY();