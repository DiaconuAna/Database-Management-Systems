use antiquitylab
go

-- PHANTOM READ
-- T2: DELAY + INSERT + COMMIT

BEGIN TRAN
WAITFOR DELAY '00:00:10'
INSERT INTO Books VALUES('A thousand ships', 'Natalie Haynes', 70)
COMMIT TRAN
