CREATE TABLE People (
    Id INT IDENTITY(1, 1) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Birthdate DATETIME NOT NULL,

    CONSTRAINT PK_People
    PRIMARY KEY (Id)
)

INSERT INTO People (Name, Birthdate) 
     VALUES ('Victor', '2000-12-07'),
            ('Steven', '1992-09-10'),
            ('Stephen', '1910-09-19'),
            ('John', '2010-01-06')

SELECT p.Name,
       DATEDIFF(YEAR, p.Birthdate, GETDATE()) AS [Age in Years],
       DATEDIFF(MONTH, p.Birthdate, GETDATE()) AS [Age in Months],
       DATEDIFF(DAY, p.Birthdate, GETDATE()) AS [Age in Days],
       DATEDIFF(MINUTE, p.Birthdate, GETDATE()) AS [Age in Minutes]
  FROM People AS p