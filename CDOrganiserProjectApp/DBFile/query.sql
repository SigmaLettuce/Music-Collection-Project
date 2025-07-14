SELECT tblArtistAlbums.albumName, tblGenres.genreName, tblArtists.artistName FROM Contents.tblArtistAlbums, Contents.tblGenres, Contents.tblArtists WHERE tblArtistAlbums.genreID = tblGenres.genreID AND tblArtistAlbums.artistID = tblArtists.artistID AND tblArtistAlbums.dateOfRelease BETWEEN '20000101' AND '20051231' 


SELECT tblBandAlbums.albumName, tblGenres.genreName, tblBands.bandName FROM Contents.tblBandAlbums, Contents.tblGenres, Contents.tblBands WHERE tblBandAlbums.genreID = tblGenres.genreID AND tblBandAlbums.bandID = tblBands.bandID AND tblBandAlbums.dateOfRelease BETWEEN '20000101' AND '20051231' 


SELECT bandName as 'All Artists' FROM Contents.tblBands UNION SELECT artistName  FROM Contents.tblArtists ORDER BY 'All Artists' asc; 