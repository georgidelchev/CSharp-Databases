CREATE TABLE People
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(3,2),
	[Weight] DECIMAL(5,2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
	VALUES ('Ivan', 2000, 1.80, 130.00, 'm', '1992-10-10', 'Just a boey.'),
	       ('Dimitar', 1000, 1.90, 110.00, 'm', '1993-05-10', 'Just a boey.'),
	       ('Siana', NULL, 1.76, 146.00, 'f', '1992-10-10', 'Just a girl.'),
	       ('Nikol', 1550, 1.55, 145.00, 'f', '1999-11-07', 'Just a girl.'),
	       ('Stavri', 1250, 1.76, 190.00, 'm', '1922-10-11', 'Just a boey.');
