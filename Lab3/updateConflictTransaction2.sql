USE antiquitylab

SELECT * FROM Books

BEGIN TRAN

	UPDATE Clients
	SET FirstName = 'deadlock Clients v2'
	WHERE Id = 1

	WAITFOR DELAY '00:00:10'


COMMIT TRAN