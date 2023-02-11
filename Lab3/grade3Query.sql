use antiquitylab
go

drop table Books
drop table Clients
drop table Acquisition

CREATE TABLE Books(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Title VARCHAR(100),
	Author VARCHAR(100),
	Price INT
	CONSTRAINT bookUnique UNIQUE(Title, Author)
);

CREATE TABLE Clients(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName VARCHAR(100),
	LastName VARCHAR(100),
	CONSTRAINT clientUnique UNIQUE(FirstName, LastName),
	Age INT
);



CREATE TABLE Acquisition(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	BookId INT FOREIGN KEY REFERENCES Books(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	ClientId INT FOREIGN KEY REFERENCES Clients(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
);


-- procedure to add a book
GO

CREATE OR ALTER PROCEDURE AddBook(@Title VARCHAR(100), @Author VARCHAR(100), @Price INT)
AS
	IF (@Title = null or @Title = ' ')
	BEGIN
		RAISERROR('Title is empty', 16, 1);
	END

	IF (@Author = null or @Author = ' ')
	BEGIN
		RAISERROR('Author is empty', 16, 1);
	END
	
	IF(@Price = null or @Price <= 0)
	BEGIN
		RAISERROR('Price is empty', 16, 1);
	END

	INSERT INTO Books VALUES(@Title, @Author, @Price)

GO

CREATE OR ALTER PROCEDURE AddClient(@FirstName VARCHAR(100), @LastName VARCHAR(100), @Age INT)
AS
	IF(@FirstName = null or @FirstName = ' ')
	BEGIN
		RAISERROR('Client first name is empty', 16, 1);
	END

	IF(@LastName = null or @LastName = ' ')
	BEGIN
		RAISERROR('Client last name is empty', 16, 1);
	END

	IF(@Age = null or @Age <= 0)
	BEGIN
		RAISERROR('Age must be > 0', 16, 1);
	END

	INSERT INTO Clients VALUES(@FirstName, @LastName, @Age)
GO

CREATE OR ALTER PROCEDURE AddAcquisition(@ClientFN VARCHAR(100), @ClientLN VARCHAR(100), @BTitle VARCHAR(100), @BAuthor VARCHAR(100))
AS

	DECLARE @BookId INT
	SET @BookId = (SELECT B.Id FROM Books B WHERE B.Author = @BAuthor AND B.Title = @BTitle)

	IF(@BookId IS NULL)
	BEGIN
		RAISERROR('Invalid book', 16, 1);
	END

	DECLARE @ClientID INT
	SET @ClientID = (SELECT C.Id FROM Clients C WHERE C.FirstName = @ClientFN AND C.LastName = @ClientLN)

	IF(@ClientID IS NULL)
	BEGIN
		RAISERROR('Invalid client', 16, 1);
	END

	INSERT INTO Acquisition VALUES(@BookId, @ClientID)
GO

-- create a stored procedure that inserts data in tables that are in a m:n relationship; 
-- if one insert fails, all the operations performed by the procedure must be rolled back 

-- Prepare test cases covering both the happy scenarios and the ones with errors (this applies to stored procedures).

CREATE OR ALTER PROCEDURE HappyScenario AS
	BEGIN TRAN
		BEGIN TRY
			EXEC AddBook 'Troy', 'Stephen Fry', 40
			EXEC AddClient 'Park', 'Amy', 19
			EXEC AddAcquisition 'Park', 'Amy', 'Troy', 'Stephen Fry'

		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
			RETURN
		END CATCH

	COMMIT TRAN
GO

-- make a client buy a non existent book
CREATE OR ALTER PROCEDURE SadScenario AS
	BEGIN TRAN
		BEGIN TRY
			EXEC AddBook 'Troy', 'Stephen Fry', 40
			EXEC AddClient 'Park', 'Amy', 19
			EXEC AddAcquisition 'Park', 'Amy', 'Mythos', 'Stephen Fry'

		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
			RETURN
		END CATCH

	COMMIT TRAN
GO

exec HappyScenario
exec SadScenario

select * from Books
select * from Clients
select * from Acquisition