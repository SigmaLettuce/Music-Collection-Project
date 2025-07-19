SELECT COUNT(tblRow.shelfRowID) as 'shelfRowID', tblShelf.shelfTag FROM Properties.tblRow, Properties.tblShelf WHERE tblRow.shelfTagID = tblShelf.shelfTagID GROUP BY tblShelf.shelfTag

