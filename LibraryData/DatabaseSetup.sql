CREATE DATABASE LibraryApp;
GO

USE [LibraryApp]
GO
CREATE TABLE [dbo].[ExceptionLogging](
	[ExceptionLoggingID] [int] IDENTITY(1,1) PRIMARY KEY,
	[StackTrace] [nvarchar](1000) NULL,
	[Message] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Url] [nvarchar](100) NULL,
	[LogDate] [datetime] NOT NULL
);

USE [LibraryApp]
GO
CREATE TABLE [Roles] (
	 RoleID int IDENTITY(1,1) PRIMARY KEY,
	 RoleName varchar(255) UNIQUE
);

USE [LibraryApp]
GO
CREATE TABLE [Users] (
	UserID int IDENTITY(1,1) PRIMARY KEY,
	RoleID int FOREIGN KEY REFERENCES Roles(RoleID),
	Email varchar(20) NOT NULL UNIQUE,
	FirstName varchar(20) NOT NULL,
	LastName varchar(20) NOT NULL,
	HashedPassword varchar(MAX),
	Salt varchar(255)
);

USE [LibraryApp]
GO
CREATE TABLE [Books] (
	BookID int IDENTITY(1,1) PRIMARY KEY,
	CheckedOutBy int FOREIGN KEY REFERENCES Users(UserID),
	OnHoldBy int FOREIGN KEY REFERENCES Users(UserID),
	Title varchar(30),
	Author varchar(30)
);

-- Create stored procedures for CRUD operations --
USE [LibraryApp]
GO
CREATE PROCEDURE [dbo].[CreateExceptionLog]
	-- Add the parameters for the stored procedure here
	@StackTrace nvarchar(1000)
    ,@Message nvarchar(100)
    ,@Source nvarchar(100)
    ,@Url nvarchar(100)
    ,@LogDate datetime
	,@parmOutExceptionLoggingID int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[ExceptionLogging] ([StackTrace],[Message],[Source],[Url],[LogDate])
	VALUES (@StackTrace,@Message,@Source,@Url,@LogDate);
	SELECT @parmOutExceptionLoggingID = SCOPE_IDENTITY();
END
GO


USE [LibraryApp]
GO
CREATE PROCEDURE CreateUser
	-- Add the parameters for the stored procedure here
	@RoleID int,
	@Email varchar(20),
	@FirstName varchar(20),
	@LastName varchar(20),
	@HashedPassword varchar(MAX),
	@Salt varchar(255),
	@paramOutUserID int output
AS
BEGIN
	-- Insert statements for procedure here
	INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (@RoleID,@Email,@FirstName,@LastName,@HashedPassword,@Salt);
	SELECT @paramOutUserID = SCOPE_IDENTITY();
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE GetAllUsers
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * FROM Users;
END
GO


USE [LibraryApp]
GO
CREATE PROCEDURE GetUserByEmail
	-- Add the parameters for the stored procedure here
	@paramEmail varchar(255)
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * 
	FROM Users
	WHERE Users.Email=@paramEmail;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE DeleteUser
	-- Add the parameters for the stored procedure here
	@paramUserID int
AS
BEGIN
	-- Insert statements for procedure here
	DELETE FROM Users
	WHERE UserID = @paramUserID;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE CreateBook
	-- Add the parameters for the stored procedure here
	@Title varchar(30)
	,@Author varchar(30)
	,@paramOutBookID int output
AS
BEGIN
	-- Insert statements for procedure here
	INSERT INTO Books (Title, Author)
	VALUES (@Title, @Author);
	SELECT @paramOutBookID = SCOPE_IDENTITY();
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE DeleteBook
	-- Add the parameters for the stored procedure here
	@paramBookID int
AS
BEGIN
	-- Insert statements for procedure here
	DELETE FROM Books
	WHERE BookID = @paramBookID;
END
GO


USE [LibraryApp]
GO
CREATE PROCEDURE GetBooks
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * FROM Books;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE CheckoutBook
	-- Add the parameters for the stored procedure here
	@BookID int
	,@UserID int
AS
BEGIN
	-- Insert statements for procedure here
	UPDATE Books
	SET Books.CheckedOutBy=@UserID
	WHERE Books.BookID=@BookID;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE ReturnBook
	-- Add the parameters for the stored procedure here
	@BookID int
AS
BEGIN
	-- Insert statements for procedure here
	UPDATE Books
	SET Books.CheckedOutBy=Books.OnHoldBy
	WHERE Books.BookID=@BookID;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE HoldBook
	-- Add the parameters for the stored procedure here
	@BookID int
	,@UserID int
AS
BEGIN
	-- Insert statements for procedure here
	UPDATE Books
	SET Books.OnHoldBy=@UserID
	WHERE Books.BookID=@BookID;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE UpdateUserPassword
	-- Add the parameters for the stored procedure here
	@Email varchar(255)
	,@NewHashedPassword varchar(MAX)
	,@NewSalt varchar(255)
AS
BEGIN
	-- Insert statements for procedure here
	UPDATE Users
	SET Users.HashedPassword=@NewHashedPassword, Users.Salt=@NewSalt
	WHERE Users.Email = @Email;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE EditUser
	-- Add the parameters for the stored procedure here
	@UserID int,
	@RoleID int,
	@Email varchar(20),
	@FirstName varchar(20),
	@LastName varchar(20)
AS
BEGIN
	-- Insert statements for procedure here
	UPDATE Users
	SET Users.RoleID=@RoleID, Users.Email=@Email, Users.FirstName=@FirstName, Users.LastName=LastName
	WHERE Users.UserID = @UserID;
END
GO

USE [LibraryApp]
GO
CREATE PROCEDURE SearchBooks
	-- Add the parameters for the stored procedure here
	@SearchExpression varchar(30)
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * FROM BOOKS
	WHERE Books.Title LIKE '%' + @SearchExpression + '%'
		  OR Books.Author LIKE '%' + @SearchExpression + '%'
END
GO


-- Insert some default data into the LibraryApp database --
INSERT INTO Roles (RoleName) VALUES ('Guest')
INSERT INTO Roles (RoleName) VALUES ('Patron')
INSERT INTO Roles (RoleName) VALUES ('Librarian')
INSERT INTO Roles (RoleName) VALUES ('Admin')

INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (4,'joseph@gmail.com','Joseph', 'Marker', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'mary@gmail.com', 'Mary', 'Holland', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'david@gmail.com', 'David', 'Duncan', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (3,'samuel@gmail.com', 'Samuel', 'Martin', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'ruth@gmail.com', 'Ruth', 'Jenkins', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');

INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (NULL, NULL, 'Ulysses', 'James Joyce');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (2, NULL, 'Don Quixote', 'Miguel de Cervantes');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (5, NULL, 'The Great Gatsby', 'F. Scott Fitzgerald');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (3, 2, 'Moby Dick', 'Herman Melville');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (NULL, NULL, 'War and Peace', 'Leo Tolstoy');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (2, 5, 'Hamlet', 'William Shakespeare');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (3, NULL, 'The Divine Comedy', 'Dante Alighieri');
INSERT INTO Books (CheckedOutBy,OnHoldBy,Title,Author)
	VALUES (NULL, NULL, 'Heart of Darkness', 'Joseph Conrad');