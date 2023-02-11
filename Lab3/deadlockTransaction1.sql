use antiquitylab
go

-- DEADLOCK
-- T1: update on table A + delay + update on table B
-- T2: update on table B + delay + update on table A

select * from Books
select * from Clients

-- transaction 1

BEGIN TRAN

UPDATE Books 
SET Title = 'deadlock Books 1'
WHERE Id =4

WAITFOR DELAY '00:00:10'

UPDATE Clients
SET FirstName = 'deadlock Clients 1'
WHERE Id = 1

COMMIT TRAN