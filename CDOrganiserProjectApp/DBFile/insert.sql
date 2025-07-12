use HomeMusicCollectionDatabase;

SET IDENTITY_INSERT Contents.tblArtists ON;

INSERT INTO Contents.tblArtists(artistID, artistName) 
VALUES
(1, 'Justin Timberlake'),
(2, 'Usher'),
(3, 'Alicia Keys'),
(4, 'Kanye West'),
(5, 'Christina Augilera'),
(6, 'James Blunt'),
(7, 'Beyoncé'),
(8, 'Avril Lavinge'),
(9, 'Lauryn Hill'),
(10, 'Aaliyah'),
(11, 'Fergie'),
(12, 'John Legend');

SET IDENTITY_INSERT Contents.tblArtists OFF;


SET IDENTITY_INSERT Contents.tblBands ON;

INSERT INTO Contents.tblBands(bandID, bandName)
VALUES
(1, 'System Of A Down'),
(2, 'Linkin Park'),
(3, 'Metallica'),
(4, 'Slayer'),
(5, 'Green Day'),
(6, 'Nirvana'),
(7, 'Pearl Jam');

SET IDENTITY_INSERT Contents.tblBands OFF;


SET IDENTITY_INSERT Properties.tblRole ON;

INSERT INTO Properties.tblRole(roleID, roleName)
VALUES 
(1, 'Visitor'),
(2, 'Administrator');

SET IDENTITY_INSERT Properties.tblRole OFF;


SET IDENTITY_INSERT Properties.tblAccounts ON;

INSERT INTO Properties.tblAccounts(personID, fName, sName, username, pw, roleID)
VALUES 
(1, 'John', 'Kramer', 'johnk', 'jigsaw123', 1),
(2, 'Clarice', 'Starling', 'agstarling', 'securepass456', 1),
(3, 'Alice', 'Johnson', 'alicej', 'alicepwd789', 1),
(4, 'Hannibal', 'Lecter', 'hanlecter', 'hansecure321', 1),
(5, 'Charlie', 'Brown', 'charlieb', 'charliepass654', 1),
(6, 'Diana', 'Evans', 'dianae', 'dianapass987', 1),
(7, 'Edward', 'Hall', 'edwardh', 'edwardpass234', 1),
(8, 'Fiona', 'Clark', 'fionac', 'fionapass567', 1),
(9, 'George', 'Lewis', 'georgel', 'georgepass890', 1),
(10, 'Hannah', 'Walker', 'hannahw', 'hannahpass345', 1),
(11, 'Jack', 'Robinson', 'jackr', 'jackpass901', 1),
(12, 'Isaac', 'White', 'isaacw', 'isaacpass678', 2);

SET IDENTITY_INSERT Properties.tblAccounts OFF;


SET IDENTITY_INSERT Properties.tblTier ON;

INSERT INTO Properties.tblTier (tierID, tierTag, tierNumericalValue)
VALUES
(1, 'S', 10),
(2, 'A',  7),
(3, 'B',  4),
(4, 'C',  1);

SET IDENTITY_INSERT Properties.tblTier OFF;


SET IDENTITY_INSERT Properties.tblFormat ON;

INSERT INTO Properties.tblFormat(formatID, formatName) 
VALUES
(1, 'CD'),
(2, 'CD-DA');

SET IDENTITY_INSERT Properties.tblFormat OFF;


SET IDENTITY_INSERT Properties.tblStorageRoom ON;

INSERT INTO Properties.tblStorageRoom(roomID, roomName)
VALUES
(1, 'Living Room'),
(2, 'Bedroom'),
(3, 'Dining Room'),
(4, 'Study Room'),
(5, 'Garage'),
(6, 'Basement');

SET IDENTITY_INSERT Properties.tblStorageRoom OFF;

INSERT INTO Properties.tblShelf(shelfTag, roomID)
VALUES
('A', 1),
('B', 2),
('C', 3),
('D', 4),
('E', 5),
('F', 5),
('G', 6);

SET IDENTITY_INSERT Properties.tblRow ON;

INSERT INTO Properties.tblRow(shelfRowID, shelfRow, shelfTag)
VALUES
(1, 1, 'A'),
(2, 2, 'A'),
(3, 3, 'A'),
(4, 4, 'A'),
(5, 5, 'A'),

(6, 1, 'B'),
(7, 2, 'B'),
(8, 3, 'B'),
(9, 4, 'B'),
(10, 5, 'B'),

(11, 1, 'C'),
(12, 2, 'C'),
(13, 3, 'C'),
(14, 4, 'C'),

(15, 1, 'D'),
(16, 2, 'D'),
(17, 3, 'D'),
(18, 4, 'D'),
(19, 5, 'D'),

(20, 1, 'E'),
(21, 2, 'E'),
(22, 3, 'E'),
(23, 4, 'E'),
(24, 5, 'E'),

(25, 1, 'F'),
(26, 2, 'F'),
(27, 3, 'F'),
(28, 4, 'F'),
(29, 5, 'F'),

(30, 1, 'G');

SET IDENTITY_INSERT Properties.tblRow OFF;


SET IDENTITY_INSERT Contents.tblGenres ON;

INSERT INTO Contents.tblGenres(genreID, genreName) 
VALUES
(1, 'Alternative Metal'),
(2, 'Nu Metal'),
(3, 'Alternative Rock'),
(4, 'Pop Rock'),
(5, 'Heavy Metal'),
(6, 'Thrash Metal'),
(7, 'Punk Rock'),
(8, 'Grunge'),
(9, 'R&B'),
(10, 'Hip-Hop'),
(11, 'Pop');

SET IDENTITY_INSERT Contents.tblGenres OFF;


SET IDENTITY_INSERT Contents.tblArtistAlbums ON;

INSERT INTO Contents.tblArtistAlbums(albumID, albumName, genreID, dateOfRelease, formatID, artistID, shelfRowID, personID, tierID, favourite, lost)
VALUES
(1, 'Justified', 9, '2002-11-05', 1, 1, 1, 2, 3, 0, 0),
(2, 'Confessions', 9, '2004-02-24', 1, 2, 2, 12, 1, 0, 0),
(3, 'Songs in A Minor', 9, '2001-02-01', 1, 3, 3, 5, 4, 0, 0),
(4, 'The College Dropout', 10, '2004-02-10', 1, 4, 10, 7, 2, 0, 0),
(5, 'Stripped', 11, '2002-11-19', 1, 5, 9, 11, 1, 0, 0),
(6, 'Back to Bedlam', 4, '2004-05-23', 1, 6, 11, 4, 3, 0, 0),
(7, 'Dangerously in Love', 9, '2003-06-24', 1, 7, 3, 9, 2, 0, 0),
(8, 'Let Go', 4, '2002-09-02', 1, 8, 12, 1, 4, 0, 0),
(9, 'FutureSex/LoveSounds', 11, '2006-09-08', 1, 1, 5, 6, 1, 0, 0),
(10, 'The Miseducation of Lauryn Hill', 9, '1998-08-25', 1, 9, 6, 8, 3, 0, 0),
(11, 'BDay', 9, '2006-09-04', 1, 7, 7, 10, 2, 0, 0),
(12, 'Aaliyah', 9, '2001-03-19', 1, 10, 8, 3, 4, 0, 0),
(13, 'The Dutchess', 11, '2006-09-19', 1, 11, 11, 2, 1, 0, 0),
(14, 'Get Lifted', 9, '2004-12-28', 1, 12, 4, 12, 2, 0, 0);

SET IDENTITY_INSERT Contents.tblArtistAlbums OFF;


SET IDENTITY_INSERT Contents.tblBandAlbums ON;

INSERT INTO Contents.tblBandAlbums(albumID, albumName, genreID, dateOfRelease, formatID, bandID, shelfRowID, personID, tierID, favourite, lost)
VALUES
(1, 'System Of A Down', 1, '1998-06-30', 1, 1, 15, 5, 4, 0, 0),
(2, 'Toxicity', 1, '2001-08-13', 2, 1, 16, 11, 2, 0, 0),
(3, 'Hypnotize', 1, '2005-11-22', 1, 1, 17, 2, 1, 0, 0),
(4, 'Mezmerize', 1, '2005-05-17', 1, 1, 18, 7, 3, 0, 0),
(5, 'Steal This Album!', 1, '2002-11-26', 2, 1, 19, 10, 2, 0, 0),
(6, 'Hybrid Theory', 2, '2000-10-24', 1, 2, 16, 3, 4, 0, 0),
(7, 'Meteora', 2, '2003-03-25', 1, 2, 17, 6, 1, 0, 0),
(8, 'Minutes to Midnight', 3, '2007-05-14', 2, 2, 15, 12, 3, 0, 0),
(9, 'The Hunting Party', 1, '2014-06-13', 2, 2, 15, 9, 2, 0, 0),
(10, 'One More Light', 4, '2017-05-19', 1, 2, 13, 4, 1, 0, 0),
(11, 'The Black Album', 5, '1991-08-12', 2, 3, 30, 8, 4, 0, 0),
(12, 'Master of Puppets', 6, '1986-03-03', 1, 3, 20, 1, 2, 0, 0),
(13, 'Ride the Lightning', 6, '1984-07-27', 1, 3, 21, 2, 3, 0, 0),
(14, '...And Justice for All', 6, '1988-09-07', 1, 3, 22, 6, 1, 0, 0),
(15, 'Kill Em All', 6, '1983-07-25', 2, 3, 23, 7, 4, 0, 0),
(16, 'Reign in Blood', 6, '1986-10-07', 2, 4, 24, 11, 3, 0, 0),
(17, 'South of Heaven', 6, '1988-07-05', 1, 4, 25, 5, 2, 0, 0),
(18, 'Seasons in the Abyss', 6, '1990-10-09', 2, 4, 26, 3, 1, 0, 0),
(19, 'God Hates Us All', 6, '2001-09-11', 2, 4, 27, 9, 2, 0, 0),
(20, 'Christ Illusion', 6, '2006-08-08', 1, 4, 28, 12, 4, 0, 0),
(21, 'American Idiot', 7, '2004-09-21', 2, 5, 18, 4, 1, 0, 0),
(22, 'Dookie', 7, '1994-02-01', 1, 5, 19, 10, 4, 0, 0),
(23, 'Insomniac', 7, '1995-10-10', 1, 5, 20, 6, 2, 0, 0),
(24, 'Warning', 7, '2000-10-03', 2, 5, 21, 2, 1, 0, 0),
(25, 'Nimrod', 7, '1997-10-14', 1, 5, 22, 8, 3, 0, 0),
(26, 'Nevermind', 8, '1991-09-24', 2, 6, 11, 7, 3, 0, 0),
(27, 'In Utero', 8, '1993-09-21', 1, 6, 12, 1, 1, 0, 0),
(28, 'Bleach', 8, '1989-06-15', 1, 6, 13, 5, 2, 0, 0),
(29, 'Ten', 8, '1991-08-27', 2, 7, 14, 12, 4, 0, 0),
(30, 'Vs.', 8, '1993-10-19', 1, 7, 30, 3, 1, 0, 0);

SET IDENTITY_INSERT Contents.tblBandAlbums OFF;
