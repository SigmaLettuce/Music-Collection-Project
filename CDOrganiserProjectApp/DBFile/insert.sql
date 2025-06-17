use HomeMusicCollectionDatabase;

SET IDENTITY_INSERT Contents.tblArtistAlbums ON;

INSERT INTO Contents.tblArtistAlbums(albumID, albumName, genreName, dateOfRelease, formatID, artistID, roomID, shelfTag, shelfRow, lost) 
VALUES
(1, 'Justified', 'R&B', '20021105', 1, 1, 3, 'A', '1A', 0),
(2, 'Confessions', 'R&B', '20040224', 1, 2, 5, 'A', '2A', 0),
(3, 'Songs in A Minor', 'R&B', '20010201', 1, 3, 6, 'A', '3A', 0),
(4, 'The College Dropout', 'Hip-Hop', '20040210', 1, 4, 1, 'B', '1B', 0),
(5, 'Stripped', 'Pop', '20021119', 1, 5, , 2, 'B', '2B', 0),
(6, 'Back to Bedlam', 'Pop Rock', '20040523', 1, 6, 4, 'B', '3B', 0),
(7, 'Dangerously in Love', 'R&B', '20030624', 1, 7, 5, 'C', '1C', 0),
(8, 'Let Go', 'Pop Rock', '20020902', 1, 8, , 1, 'C', '2C', 0),
(9, 'FutureSex/LoveSounds', 'Pop/R&B', '20060908', 1, 1, 6, 'D', '1D', 0),
(10, 'The Miseducation of Lauryn Hill', 'R&B', '19980825', 1, 9, 3, 'D', '2D', 0),
(11, 'BDay', 'R&B', '20060904', 1, 7, 2, 'E', '1E', 0),
(12, 'Aaliyah', 'R&B', '20010319', 1, 10, 6, 'E', '2E', 0),
(13, 'The Dutchess', 'Pop', '20060919', 1, 11, 5, 'F', '1F', 0),
(14, 'Get Lifted', 'R&B', '20041228', 1, 12, 2, 'F', '2F', 0);


SET IDENTITY_INSERT Contnets.tblArtistAlbums OFF;

SET IDENTITY_INSERT Contents.tblBandAlbums ON;

INSERT INTO Contents.tblBandAlbums(albumID, albumName, genreName, dateOfRelease, formatID, bandID, roomID, shelfTag, shelfRow, lost) 
VALUES
(1, 'System Of A Down', 'Alternative Metal', '19980630', 1, 1, 5, 'A', '1A', 0),
(2, 'Toxicity', 'Alternative Metal', '20010813', 2, 1, 2, 'A', '2A', 0),
(3, 'Hypnotize', 'Alternative Metal', '20051122', 1, 1, 6, 'A', '3A', 0),
(4, 'Mezmerize', 'Alternative Metal', '20050517', 1, 1, 1, 'A', '4A', 0),
(5, 'Steal This Album!', 'Alternative Metal', '20021126', 2, 1, 3, 'A', '5A', 0),
(6, 'Hybrid Theory', 'Nu Metal', '20001024', 1, 2, 2, 'B', '1B', 0),
(7, 'Meteora', 'Nu Metal', '20030325', 1, 2, 3, 'B', '2B', 0),
(8, 'Minutes to Midnight', 'Alternative Rock', '20070514', 2, 2, 6, 'B', '3B', 0),
(9, 'The Hunting Party', 'Alternative Metal', '20140613', 2, 2, 4, 'B', '4B', 0), 
(10, 'One More Light', 'Pop Rock', '20170519', 1, 2, 5, 'B', '5B', 0),
(11, 'The Black Album', 'Heavy Metal', '19910812', 2, 3, 1, 'C', '1C', 0),
(12, 'Master of Puppets', 'Thrash Metal', '19860303', 1, 3, 6, 'C', '2C', 0),
(13, 'Ride the Lightning', 'Thrash Metal', '19840727', 1, 3, 2, 'C', '3C', 0),
(14, '...And Justice for All', 'Thrash Metal', '19880907', 1, 3, 3, 'C', '4C', 0),
(15, 'Kill Em All', 'Thrash Metal', '19830725', 2, 3, 5, 'C', '4C', 0),
(16, 'Reign in Blood', 'Thrash Metal', '19861007', 2, 4, 2, 'D', '1D', 0),
(17, 'South of Heaven', 'Thrash Metal', '19880705', 1, 4, 6, 'D', '2D', 0),
(18, 'Seasons in the Abyss', 'Thrash Metal', '19901009', 2, 4, 1, 'D', '3D', 0),
(19, 'God Hates Us All', 'Thrash Metal', '20010911', 2, 4, 5, 'D', '4D', 0),
(20, 'Christ Illusion', 'Thrash Metal', '20060808', 1, 4, 3, 'D', '5D', 0),
(21, 'American Idiot', 'Punk Rock', '20040921', 2, 5, 6, 'E', '1E', 0),
(22, 'Dookie', 'Punk Rock', '19940201', 1, 5, 1, 'E', '2E', 0),
(23, 'Insomniac', 'Punk Rock', '19951010', 1, 5, 3, 'E', '3E', 0),
(24, 'Warning', 'Punk Rock', '20001003', 2, 5, 5, 'E', '4E', 0),
(25, 'Nimrod', 'Punk Rock', '19971014', 1, 5, 2, 'E', '5E', 0),
(26, 'Nevermind', 'Grunge', '19910924', 2, 6, 3, 'F', '1F', 0),
(27, 'In Utero', 'Grunge', '19930921', 1, 6, 6, 'F', '2F', 0),
(28, 'Bleach', 'Grunge', '19890615', 1, 6, 1, 'F', '3F', 0),
(29, 'Ten', 'Grunge', '19910827', 2, 7, 2, 'G', '1G', 0),
(30, 'Vs.', 'Grunge', '19931019', 1, 7, 5, 'G', '1G', 0);

SET IDENTITY_INSERT Contnets.tblBandAlbums OFF;
