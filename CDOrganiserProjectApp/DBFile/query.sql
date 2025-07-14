SELECT tblTier.tierTag FROM Contents.tblArtistReviews, Contents.tblArtistAlbums, Contents.tblArtists, Properties.tblTier WHERE tblArtists.artistName = @search AND tblArtistReviews.tierID = tblTier.tierID AND tblArtistReviews.albumID = tblArtistAlbums.albumID AND tblArtistAlbums.artistID = tblArtists.artistID

SELECT tblTier.tierTag 
FROM Contents.tblBandReviews, Contents.tblBandAlbums, Contents.tblBands, Properties.tblTier 
WHERE tblBands.bandName = @search
AND tblBandReviews.tierID = tblTier.tierID 
AND tblBandReviews.albumID = tblBandAlbums.albumID
AND tblBandAlbums.bandID = tblBands.bandID