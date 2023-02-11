use antiquitylab
go

-- Deadlocks --

select * from Books
select * from Clients

-- transaction 2

BEGIN TRAN

UPDATE Clients
SET FirstName = 'deadlock Clients 1'
WHERE Id = 1

WAITFOR DELAY '00:00:10'

UPDATE Books 
SET Title = 'deadlock Books 1'
WHERE Id = 4


COMMIT TRAN