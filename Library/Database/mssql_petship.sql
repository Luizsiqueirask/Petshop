-- Create database
CREATE DATABASE Petship;

-- Drop database
DROP DATABASE Petship;

-- Use Database
USE Petship;

-- Drop Tables
DROP TABLE Contacts;
DROP TABLE Pictures;
DROP TABLE Addresses;
DROP TABLE Person;
DROP TABLE Health;
DROP TABLE [Services];
DROP TABLE Pet;

----------------------------------------------------------
				  /* CREATE TABLES */
----------------------------------------------------------

------------------------- Person -------------------------


-- Creating Contacts

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Contacts' and type in (N'U'))

CREATE TABLE [dbo].[Contacts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Email NVARCHAR(250) NOT NULL,
	Mobile NVARCHAR(250) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Contacts - Exists this table !!!'
GO

-- Creating Pictures

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pictures' and type in (N'U'))

CREATE TABLE [dbo].[Pictures](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Tag] NVARCHAR(255) NULL,
	[Path] NVARCHAR(255) NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Pictures - Exists this table !!!'
GO

-- Creating Addresses

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Addresses' and type in (N'U'))

CREATE TABLE [dbo].[Addresses](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Country] NVARCHAR(255) NOT NULL,
	[States] NVARCHAR(255) NOT NULL,
	[City] NVARCHAR(255) NOT NULL,
	[Neighborhoods] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Addresses - Exists this table !!!'
GO

-- Creating Person

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Person' and type in (N'U'))

CREATE TABLE [dbo].[Person] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[FirstName] NVARCHAR(250) NOT NULL,
	[LastName] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Birthday] DATE NOT NULL,
	[PictureId] INT NULL,
	[AddressId] INT NOT NULL,
	[ContactId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (PictureId) REFERENCES [dbo].[Pictures](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ContactId) REFERENCES [dbo].[Contacts](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (AddressId) REFERENCES [dbo].[Addresses](Id) ON UPDATE CASCADE ON DELETE CASCADE
);
ELSE
	PRINT 'Person - Exists this table !!!'
GO

------------------------- Pet ----------------------------

-- Creating Images

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Images' and type in (N'U'))

CREATE TABLE [dbo].[Images](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Tag] NVARCHAR(255) NULL,
	[Path] NVARCHAR(255) NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
ELSE
	PRINT 'Images - Exists this table !!!'
GO

-- Creating Health

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Health' and type in (N'U'))

CREATE TABLE [dbo].[Health](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Status] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Health - Exists this table !!!'
GO

-- Creating Services

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Services' and type in (N'U'))

CREATE TABLE [dbo].[Services](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Category] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Services - Exists this table !!!'
GO

-- Creating Pet

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pet' and type in (N'U'))

CREATE TABLE [dbo].[Pet] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(250) NOT NULL,
	[Type] NVARCHAR(250) NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Birthday] DATE NOT NULL,
	[ImageId] INT NULL,
	[HealthId] INT NOT NULL,
	[ServiceId] INT NOT NULL,
	[PersonId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (ImageId) REFERENCES [dbo].[Images](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (HealthId) REFERENCES [dbo].[Health](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ServiceId) REFERENCES [dbo].[Services](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (PersonId) REFERENCES [dbo].[Person](Id) ON UPDATE CASCADE ON DELETE CASCADE
);
ELSE
	PRINT 'Pet - Exists this table !!!'
GO

----------------------------------------------------------
			  /* Crud Procedure Operation */
----------------------------------------------------------


/* ********************* Person ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListPerson]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM [dbo].[Person] p1
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
END
GO;

/* Detail */
CREATE PROCEDURE [dbo].[GetPerson]
	@IdPerson AS INT
AS BEGIN
	SELECT 
		-- Person
		p1.Id,
		p1.FirstName,
		p1.LastName,
		p1.Age,
		p1.Birthday,
		-- Address
		p1.AddressId,
		-- Pictures
		p2.Id,
		p2.[Tag],
		p2.[Path],
		-- Contacts
		c2.Id,
		c2.Email,
		c2.Mobile	
	FROM Person p1
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
	WHERE p1.Id = @IdPerson
END
GO;

/* Create */
CREATE PROCEDURE [dbo].[PostPerson]
	-- Person
	@FirstName AS NVARCHAR(250),
	@LastName AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT,
	-- Contacts
	@Email AS NVARCHAR(250),
	@Mobile AS NVARCHAR(250),
	@ContactId AS INT,
	-- Pictures
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@PictureId AS INT,
	-- Addresses
	@AddressId AS INT,
	@Country AS NVARCHAR(250),
	@States AS NVARCHAR(250),
	@City AS NVARCHAR(250),
	@Neighborhoods AS NVARCHAR(250)
AS BEGIN
	-- Pictures
	INSERT INTO [dbo].[Pictures]([Tag], [Path]) 
	VALUES (@Tag, @Path);
	-- Contacts
	INSERT INTO [dbo].[Contacts]([Email], [Mobile]) 
	VALUES (@Email, @Mobile);
	-- Addresses
	INSERT INTO [dbo].[Addresses]([Country], [States], [City], [Neighborhoods]) 
	VALUES (@Country, @States, @City, @Neighborhoods);
	-- Person
	INSERT INTO [dbo].[Person]([FirstName], [LastName], [Age], [Genre], [Birthday], [PictureId], [AddressId], [ContactId])
	VALUES (@FirstName, @LastName, @Age, @Genre, Convert(date, @Birthday), @PictureId, @AddressId, @ContactId);
END
GO;

/* Update */
CREATE PROCEDURE [dbo].[PutPerson]
	@IdPerson AS INT,
	-- Pictures
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@PictureId AS INT,
	-- Contacts
	@Email AS NVARCHAR(255),
	@Mobile AS NVARCHAR(255),
	@ContactId AS INT,
	-- Person
	@FirstName AS NVARCHAR(250),
	@LastName AS NVARCHAR(250),
	@Age AS INT,
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	-- Addresses
	@AddressId AS INT,
	@Country AS NVARCHAR(250),
	@States AS NVARCHAR(250),
	@City AS NVARCHAR(250),
	@Neighborhoods AS NVARCHAR(250)

AS BEGIN
	-- Pictures
	UPDATE [dbo].[Pictures] SET 
		[Tag] = @Tag,
		[Path] = @Path
	WHERE Id = @PictureId;
	
	-- Contacts
	UPDATE [dbo].[Contacts] SET
		Email = @Email,
		Mobile = @Mobile 
	WHERE Id = @ContactId;

	-- Addresses
	UPDATE  [dbo].[Addresses] SET 
		[Country] = @Country,
		[States] = @States,
		[City] = @City,
		[Neighborhoods] = @Neighborhoods
	WHERE Id = @AddressId;
	
	-- Person
	UPDATE [dbo].[Person] SET 
		FirstName = @FirstName,
		LastName = @LastName,
		Age = @Age,
		Genre = @Genre,
		Birthday = Convert(date, @Birthday),
		PictureId = @PictureId,
		AddressId = @AddressId, 
		ContactId = @ContactId
	WHERE Id = @IdPerson;
END
GO;

/* Delete */
CREATE PROCEDURE [dbo].[DeletePerson]	
	@IdPerson AS INT
AS BEGIN 
	DELETE p1 FROM Person p1
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
	WHERE p1.Id = @IdPerson
END
GO;


/* ********************* Pet ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListPet]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM Pet p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id
	-- Services
	LEFT JOIN [dbo].[Services] s1
	ON p1.ServiceId = s1.Id
END
GO;

/* Detail */
CREATE PROCEDURE [dbo].[GetPet]
	@IdPet AS INT
AS BEGIN
	SELECT 
		-- Person
		p1.Id,
		p1.[Name],
		p1.[Type],
		p1.Age,
		p1.Birthday,
		p1.Genre,
		-- Health
		h1.[Status],
		-- Pictures
		i1.Id,
		i1.[Tag],
		i1.[Path],
		-- Services
		s1.Category	
	FROM Pet p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id
	-- Services
	LEFT JOIN [dbo].[Services] s1
	ON p1.ServiceId = s1.Id
	WHERE p1.Id = @IdPet
END
GO;

/* Create */
CREATE PROCEDURE [dbo].[PostPet]
	-- Pet
	@Name AS NVARCHAR(250),
	@Type AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT,
	@PersonId AS INT,
	-- Images
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@ImageId AS INT,
	-- Health
	@Status AS NVARCHAR(250),
	@HealthId AS INT,
	-- Services
	@Category AS NVARCHAR(250),
	@ServiceId AS INT

AS BEGIN
	-- Images
	INSERT INTO [dbo].[Images]([Tag], [Path]) 
	VALUES (@Tag, @Path);
	-- Contacts
	INSERT INTO [dbo].[Health]([Status]) 
	VALUES (@Status);
	-- Addresses
	INSERT INTO [dbo].[Services]([Category]) 
	VALUES (@Category);
	-- Person
	INSERT INTO [dbo].[Pet]([Name], [Type], [Age], [Genre], [Birthday], [ImageId], [HealthId], [ServiceId], [PersonId])
	VALUES (@Name, @Type, @Age, @Genre, Convert(date, @Birthday), @ImageId, @HealthId, @ServiceId, @PersonId);
END
GO;

/* Update */
CREATE PROCEDURE [dbo].[PutPet]
	@IdPet AS INT,
	-- Person
	@Name AS NVARCHAR(250),
	@Type AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@PersonId AS INT,
	@Birthday AS DATE,
	@Age AS INT,	
	-- Images
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@ImageId AS INT,
	-- Health
	@HealthId AS INT,
	@Status AS NVARCHAR(250),
	-- Services
	@ServiceId AS INT,
	@Category AS NVARCHAR(250)

AS BEGIN
	-- Images
	UPDATE [dbo].[Images] SET 
		[Tag] = @Tag,
		[Path] = @Path
	WHERE Id = @ImageId;
	
	-- Health
	UPDATE [dbo].[Health] SET
		[Status] = @Status
	WHERE Id = @HealthId;

	-- Services
	UPDATE  [dbo].[Services] SET 
		[Category] = @Category
	WHERE Id = @ServiceId;
	
	-- Pet
	UPDATE [dbo].[Pet] SET 
		[Name] = @Name,
		[Type] = @Type,
		Age = @Age,
		Genre = @Genre,
		Birthday = Convert(date, @Birthday),
		ImageId = @ImageId,
		HealthId = @HealthId,
		ServiceId = @ServiceId,
		PersonId = @PersonId
	WHERE Id = @IdPet;
END
GO;

/* Delete */
CREATE PROCEDURE [dbo].[DeletePet]	
	@IdPet AS INT
AS BEGIN 
	DELETE p1 FROM Pet p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id
	-- Services
	LEFT JOIN [dbo].[Services] s1
	ON p1.ServiceId = s1.Id
	WHERE p1.Id = @IdPet
END
GO;

/* ********************* Person ********************* */

/* List */

EXEC [dbo].[ListPerson];

/* Details */

EXEC [dbo].[GetPerson] @IdPerson = 1;

/* Create */

EXEC [dbo].[PostPerson] @FirstName = 'Luiz', @LastName = 'Siqueira', @Genre = 'Male',  @Birthday = '1990-01-28', @Age = '31',
	 @Email = 'luiz@siqueira.psk', @Mobile = '21975918265', @ContactId = 1, 
	 @Tag = 'MyPicturePerson', @Path = '../Pictures/person.png',  @PictureId = 1, 
	 @AddressId = 1, @Country = 'Brasil', @States = 'Rio de Janeiro', @City = 'Rio de Janeiro', @Neighborhoods = 'Leme';

/* Update */

EXEC [dbo].[PutPerson] @IdPerson = 1, @FirstName = 'Luiz', @LastName = 'Siqueira', @Genre = 'Male',  @Birthday = '1990-01-28', @Age = '31',
	 @Email = 'luiz@siqueira.psk', @Mobile = '21975918265', @ContactId = 1,
	 @Tag = 'MyPicturePerson', @Path = '../Pictures/person.png',  @PictureId = 1, 
	 @AddressId = 1, @Country = 'Brasil', @States = 'Rio de Janeiro', @City = 'Rio de Janeiro', @Neighborhoods = 'Leme';

/* Delete */

EXEC [dbo].[DeletePerson] @IdPerson = 1;


/* ********************* Pet ********************* */

/* List */

EXEC [dbo].[ListPet];

/* Details */

EXEC [dbo].[GetPet] @IdPet = 1;

/* Create */

EXEC [dbo].[PostPet] @Name = 'Negão', @Type = 'Dog', @Genre = 'M', @Birthday = '2015-05-10', @Age = '10', 
	 @Tag = 'negao', @Path = '~/Pictures/negao.png', @ImageId = 1,
	 @Status = 'Bad', @HealthId = 1, @Category = 'Banho',  @ServiceId = 1, @PersonId = 1;

/* Update */

EXEC [dbo].[PutPet] @IdPet = 1,@Name = 'Negão', @Type = 'Dog', @Genre = 'M', @Birthday = '2015-05-10', @Age = '10', 
	 @Tag = 'negao', @Path = '~/Pictures/negao.png', @ImageId = 1,
	 @Status = 'Bad', @HealthId = 1, @Category = 'Banho',  @ServiceId = 1, @PersonId = 1;

/* Delete */

EXEC [dbo].[DeletePet] @IdPet = 1;
