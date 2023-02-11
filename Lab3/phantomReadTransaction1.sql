use antiquitylab
go

-- PHANTOM READ
-- T1 executes a query twice and it gets a different number of rows
-- in the result set because, in the meantime, T2 inserts a new row that matches the
-- WHERE clause of T1's query

-- Isolation level: repeatable read => Solution: SERIALIZABLE

-- T1: SELECT + DELAY + SELECT

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM Books
WAITFOR DELAY '00:00:15'
SELECT * FROM Books
COMMIT TRAN


