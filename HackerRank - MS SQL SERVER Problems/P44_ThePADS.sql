CREATE TABLE Occupations
(
    Id         INT PRIMARY KEY IDENTITY,
    Name       NVARCHAR(30) NOT NULL,
    Occupation NVARCHAR(30) NOT NULL
)

INSERT INTO Occupations(Name, Occupation)
    VALUES ('Samantha', 'Doctor'),
           ('Julia', 'Actor'),
           ('Maria', 'Actor'),
           ('Meera', 'Singer'),
           ('Ashley', 'Professor'),
           ('Ketty', 'Professor'),
           ('Cristeen', 'Professor'),
           ('Jane', 'Actor'),
           ('Jenny', 'Doctor'),
           ('Priya', 'Singer')

SELECT o.Name + '(' + LEFT(o.Occupation, 1) + ')'
    FROM Occupations AS o
    ORDER BY o.Name

SELECT 'There are a total of ' +
       CAST(COUNT(*) AS NVARCHAR(10)) + ' ' +
       LOWER(o.Occupation) + 's.'
    FROM Occupations AS o
    GROUP BY o.Occupation
    ORDER BY COUNT(*)
