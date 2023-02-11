use antiquitylab
go

-- create a stored procedure that inserts data in tables that are in a m:n relationship; 
-- if an insert fails, try to recover as much as possible from the entire operation: 
-- for example, if the user wants to add a book and its authors, succeeds creating the authors,  
-- but fails with the book, the authors should remain in the database (grade 5);

-- create a log table to keep track of what was inserted
-- and what was not

CREATE TABLE Logs(
	executedOperation VARCHAR(100),
	error VARCHAR(1000),
	executionTime DATETIME
)
GO

CREATE OR ALTER PROCEDURE AddLog(@executedOperation VARCHAR(100), @error VARCHAR(1000))
AS
	INSERT INTO Logs VALUES(@executedOperation, @error, GETDATE())
GO

CREATE OR ALTER PROCEDURE RecoverableScenario(
	-- for book
	@Title VARCHAR(100), @Author VARCHAR(100), @Price INT,
	-- for client
	@FirstName VARCHAR(100), @LastName VARCHAR(100), @Age INT)
AS
	BEGIN TRAN
		DECLARE @error VARCHAR(1000)

		-- insert the book first
		BEGIN TRY
			EXEC AddBook @Title, @Author, @Price
			EXEC AddLog 'Book added successfully', ' '
		END TRY
		-- if sth goes wrong, log it
		BEGIN CATCH 
			SELECT @error = ERROR_MESSAGE()
			EXEC AddLog 'Book added FAILURE', @error
			COMMIT TRAN -- finish things here
			RETURN
		END CATCH

		-- insert the client afterwards
		BEGIN TRY
			EXEC AddClient @FirstName, @LastName, @Age
			EXEC AddLog 'Client added successfully', ' '
		END TRY
		-- log what goes wrong
		BEGIN CATCH
			SELECT @error = ERROR_MESSAGE()
			EXEC AddLog 'Client added FAILURE', @error
			COMMIT TRAN
			RETURN
		END CATCH

		-- now the acquisition
		BEGIN TRY
			EXEC AddAcquisition @FirstName, @LastName, @Title, @Author
			EXEC AddLog 'Acquisition added successfully', ' ' 
		END TRY
		-- and if sth goes wrong
		BEGIN CATCH
			SELECT @error = ERROR_MESSAGE()
			EXEC AddLog 'Acquisition added FAILURE', @error
			COMMIT TRAN
			RETURN
		END CATCH

		EXEC AddLog 'SUCCESS', ''
	COMMIT TRAN
GO


-- execute the happy scenario 
-- book: Song of Achilles, Madeline Miller, 120
-- client: George, Russell, 23

EXEC RecoverableScenario 'Song of Achilles', 'Madeline Miller', 120, 'George', 'Russell', 23
select * from Logs
select * from Books
select * from Clients
select * from Acquisition

-- execute the sad scenario :'(
-- try to add a client that already exists
EXEC RecoverableScenario 'Stardust', 'Neil Gaiman', 100, 'George', 'Russell', 23
select * from Logs
select * from Books
select * from Clients
select * from Acquisition

DROP TABLE Logs
DROP TABLE Books
DROP TABLE Clients
DROP TABLE Acquisition