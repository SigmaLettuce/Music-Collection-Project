SELECT reviewID, albumName, fName, tierTag, favourite
FROM Contents.tblBandReviews, Contents.tblBandAlbums, Properties.tblAccounts, Properties.tblTier
WHERE tblBandReviews.albumID = tblBandAlbums.albumID 
AND tblBandReviews.personID = tblAccounts.personID
AND tblBandReviews.tierID = tblTier.tierID
