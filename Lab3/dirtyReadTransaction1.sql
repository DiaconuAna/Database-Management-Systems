use antiquitylab
go

-- Dirty Read
-- A dirty read happens when one transaction is permitted to read data that has been modified by another transaction that 
-- has not yet been committed. In most cases this would not cause a problem. However, if the first transaction is rolled back after
-- the second reads the data, the second transaction has dirty data that does not exist anymore

SELECT * FROM Books

BEGIN TRANSACTION
UPDATE Books
SET author = 'Neil Gaiman'
WHERE author = 'Stephen Fry'

WAITFOR DELAY '00:00:10'
ROLLBACK TRANSACTION