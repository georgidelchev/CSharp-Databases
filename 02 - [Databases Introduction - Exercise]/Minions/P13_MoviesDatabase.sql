-- CREATE NEW DATABASE
CREATE DATABASE Movies

-- CREATE NEW TABLE
CREATE TABLE Directors
(
	Id INT IDENTITY PRIMARY KEY,
	DirectorName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

-- CREATE NEW TABLE
CREATE TABLE Genres
(
	Id INT IDENTITY PRIMARY KEY,
	GenreName NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX)
)

-- CREATE NEW TABLE
CREATE TABLE Categories
(
	Id INT IDENTITY PRIMARY KEY,
	CategoryName NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX)
)

-- CREATE NEW TABLE
CREATE TABLE Movies
(
	Id INT IDENTITY PRIMARY KEY,
	Title VARCHAR(20) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear DATE NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT NOT NULL,
	CategoryId INT NOT NULL,
	Rating DECIMAL(5, 2),
	Notes NVARCHAR(MAX)

	CONSTRAINT FK_Movies_DirectorId -- ADD NEW CONSTRAINT WITH THAT NAME
		FOREIGN KEY (DirectorId) -- CREATING FOREIGN KEY FOR DirectorId COLUMN
			REFERENCES Directors (Id), -- REFERENCES TO DIRECTORS TABLE ID COLUMN

	CONSTRAINT FK_Movies_GenreId
		FOREIGN KEY (GenreId)
			REFERENCES Genres (Id),

	CONSTRAINT FK_Movies_CategoryId
		FOREIGN KEY (CategoryId)
			REFERENCES Categories (Id)
)

-- DIRECTORS
INSERT INTO Directors (DirectorName, Notes)
	VALUES ('Ivancho1', 'TODO: Pay salaries'),
		   ('Ivancho2', 'TODO: Pay salaries'),
		   ('Ivancho3', 'TODO: Pay salaries'),
		   ('Ivancho4', 'TODO: Pay salaries')

INSERT INTO Directors (DirectorName)
	VALUES ('Ivancho5')

-- GENRES
INSERT INTO Genres (GenreName, Notes)
	VALUES ('Horror1', 'TODO: Make it less scary'),
		   ('Horror2', 'TODO: Make it less scary'),
		   ('Horror3', 'TODO: Make it less scary'),
		   ('Horror4', 'TODO: Make it less scary')

INSERT INTO Genres (GenreName)
	VALUES ('Horror5')

-- CATEGORIES
INSERT INTO Categories (CategoryName, Notes)
	VALUES ('1Above18', 'TODO: IDK...'),
		   ('2Above18', 'TODO: IDK...'),
		   ('3Above18', 'TODO: IDK...'),
		   ('4Above18', 'TODO: IDK...')

INSERT INTO Categories (CategoryName)
	VALUES ('5Above18')

-- MOVIES
INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
	VALUES ('Supernaturale1', 1, '1990-05-10', '10:25:45', 1, 1, 105.55, 'TODO: IDK...'),
		   ('Supernaturale2', 2, '1990-05-20', '10:25:45', 2, 2, 106.55, 'TODO: IDK...'),
		   ('Supernaturale3', 3, '1990-05-30', '10:25:45', 3, 3, 101.55, 'TODO: IDK...'),
		   ('Supernaturale4', 4, '1990-05-20', '10:25:45', 4, 4, 102.55, 'TODO: IDK...')

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating)
	VALUES ('Supernaturale5', 5, '1990-05-10', '10:25:45', 5, 5, 103.55)

-- CHECK RESULTS
SELECT * FROM Directors
SELECT * FROM Genres
SELECT * FROM Categories
SELECT * FROM Movies
