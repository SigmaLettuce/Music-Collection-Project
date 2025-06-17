-- This is a query that is used to test the functionality of a query.

SELECT tblAlbums.albumName, tblAlbums.genreName, tblAlbums.dateOfRelease, tblFormat.formatName, tblArtists.artistName, tblBands.bandName, tblStorageRoom.roomName, tblAlbums.shelfTag, tblAlbums.shelfRow
FROM Contents.tblAlbums
JOIN Properties.tblFormat ON tblAlbums.formatID = tblFormat.formatID
LEFT JOIN Contents.tblArtists ON tblAlbums.artistID = tblArtists.artistID
LEFT JOIN Contents.tblBands ON tblAlbums.bandID = tblBands.bandID
JOIN Properties.tblStorageRoom ON tblAlbums.roomID = tblStorageRoom.roomID
ORDER BY tblAlbums.albumID asc;