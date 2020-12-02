CREATE TABLE People (
    Id INT IDENTITY(1, 1),
    Name NVARCHAR(200) NOT NULL,
    Picture VARBINARY(MAX),
    Height DECIMAL(3, 2),
    Weight DECIMAL(5, 2),
    Gender CHAR(1) NOT NULL,
    Birthdate DATE NOT NULL,
    Biography NVARCHAR(MAX),

    CONSTRAINT PK_People
    PRIMARY KEY (Id)
)

INSERT INTO People (Name, Picture, Height, Weight, Gender, Birthdate, Biography)
     VALUES ('Ivan', 1000, 1.7999999, 100.0001, 'm', '1984-02-29', 'I am Ivan'),
            ('Georgi', NULL, 1.93, 98.44, 'm', '1988-04-16', 'I am Georgi'),
            ('Mariq', 12345, 1.666666, 66.6666, 'f', '1990-06-25', 'I am Mariq'),
            ('Dimitar', NULL, 1.854, 78, 'm', '1985-05-05', 'I am Dimitar'),
            ('Petar', 5555, 1.76, 88.2344, 'm', '1993-07-30', 'I am Petar')