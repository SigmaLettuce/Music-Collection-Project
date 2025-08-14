
CREATE Schema Contents;
GO 

CREATE Schema Properties;
GO
 

CREATE TABLE Contents.tblArtists (
	artistID INT IDENTITY(1,1) PRIMARY KEY,
	artistName VARCHAR(50) NOT NULL
);

CREATE TABLE Contents.tblBands (
	bandID INT IDENTITY(1,1) PRIMARY KEY,
	bandName VARCHAR(50) NOT NULL
);

CREATE TABLE Properties.tblRole (
	roleID INT IDENTITY(1,1) PRIMARY KEY,
	roleName VARCHAR(255) NOT NULL
);

CREATE TABLE Properties.tblAccounts (
	personID INT IDENTITY(1,1) PRIMARY KEY,
	fName VARCHAR(30) NOT NULL,
	sName VARCHAR(30) NOT NULL,
	username VARCHAR(15) NOT NULL,
	pw VARCHAR(30) NOT NULL,
	roleID INT NOT NULL
	FOREIGN KEY (roleID) REFERENCES Properties.tblRole (roleID) 
);

CREATE TABLE Properties.tblTier (
	tierID INT IDENTITY(1,1) PRIMARY KEY,
	tierTag VARCHAR(1) NOT NULL,
	tierNumericalValue INT NOT NULL
);

CREATE TABLE Contents.tblGenres (
	genreID INT IDENTITY(1,1) PRIMARY KEY,
	genreName VARCHAR(20)
);

CREATE TABLE Properties.tblFormat (
	formatID INT IDENTITY(1,1) PRIMARY KEY,
	formatName VARCHAR(15) NOT NULL
);

CREATE TABLE Properties.tblStorageRoom (
	roomID INT IDENTITY(1,1) PRIMARY KEY,
	roomName VARCHAR(20) NOT NULL
);

CREATE TABLE Properties.tblShelf (
	shelfTagID INT IDENTITY(1,1) PRIMARY KEY,
	shelfTag VARCHAR(1) NOT NULL, 
	roomID INT NOT NULL,
	FOREIGN KEY (roomID) REFERENCES Properties.tblStorageRoom (roomID) 
);

CREATE TABLE Properties.tblRow (
	shelfRowID INT IDENTITY(1,1) PRIMARY KEY,
	shelfRow INT NOT NULL,
	shelfTagID INT NOT NULL,
	FOREIGN KEY (shelfTagID) REFERENCES Properties.tblShelf (shelfTagID)
);

CREATE TABLE Contents.tblArtistAlbums (
	albumID INT IDENTITY(1,1) PRIMARY KEY,
	albumName VARCHAR(50) NOT NULL,
	genreID INT NOT NULL,
	dateOfRelease DATE NOT NULL,
	formatID INT NOT NULL,
	artistID INT NOT NULL,
	shelfRowID INT NOT NULL,
	lost BIT NOT NULL,

	FOREIGN KEY (genreID) REFERENCES Contents.tblGenres (genreID), 
	FOREIGN KEY (formatID) REFERENCES Properties.tblFormat (formatID),
	FOREIGN KEY (artistID) REFERENCES Contents.tblArtists (artistID),
	FOREIGN KEY (shelfRowID) REFERENCES Properties.tblRow (shelfRowID) 

);

CREATE TABLE Contents.tblBandAlbums (
	albumID INT IDENTITY(1,1) PRIMARY KEY,
	albumName VARCHAR(50) NOT NULL,
	genreID INT NOT NULL,
	dateOfRelease DATE NOT NULL,
	bandID INT NOT NULL,
	formatID INT NOT NULL,
	shelfRowID INT NOT NULL,
	lost BIT NOT NULL,

	FOREIGN KEY (genreID) REFERENCES Contents.tblGenres (genreID),
	FOREIGN KEY (formatID) REFERENCES Properties.tblFormat (formatID),
	FOREIGN KEY (bandID) REFERENCES Contents.tblBands (bandID),
	FOREIGN KEY (shelfRowID) REFERENCES Properties.tblRow (shelfRowID) 

);

CREATE TABLE Contents.tblArtistReviews (
	reviewID INT IDENTITY(1,1),
	albumID INT NOT NULL,
	personID INT NOT NULL,
	tierID INT NOT NULL,
	favourite BIT NOT NULL,
	
	FOREIGN KEY (albumID) REFERENCES Contents.tblArtistAlbums (albumID),
	FOREIGN KEY (personID) REFERENCES Properties.tblAccounts (personID),
	FOREIGN KEY (tierID) REFERENCES Properties.tblTier (tierID) 

);

CREATE TABLE Contents.tblBandReviews (
	reviewID INT IDENTITY(1,1),
	albumID INT NOT NULL,
	personID INT NOT NULL,
	tierID INT NOT NULL,
	favourite BIT NOT NULL,
	
	FOREIGN KEY (albumID) REFERENCES Contents.tblBandAlbums (albumID),
	FOREIGN KEY (personID) REFERENCES Properties.tblAccounts (personID),
	FOREIGN KEY (tierID) REFERENCES Properties.tblTier (tierID) 

);
