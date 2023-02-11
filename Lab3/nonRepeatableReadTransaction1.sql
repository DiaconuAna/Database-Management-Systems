use antiquitylab
go

-- NON-REPEATABLE READS
-- T1 reads the same data twice: SELECT + DELAY + SELECT
-- T2 updates the data between the 2 reads of T1: DELAY + UPDATE + COMMIT

-- happens inside the default isolation level: ReadCommitted
-- as the SharedLock is released as soon as SELECT is performed

-- SOLUTION: set transaction isolation level to RepeatableRead

SELECT * FROM Books

BEGIN TRAN
WAITFOR DELAY '00:00:10'
UPDATE Books
SET Price = 50
WHERE Author = 'Stephen Fry'
COMMIT TRAN
