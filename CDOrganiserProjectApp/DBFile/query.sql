SELECT reviewID, albumName, tierTag, fName, favourite FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier WHERE tblBandReviews.albumID = tblBandAlbums.albumID AND tblBandReviews.tierID = tblTier.tierID AND tblBandReviews.personID = tblAccounts.personID

UPDATE Contents.tblBandReviews SET tierID = 2 WHERE reviewID = 1001