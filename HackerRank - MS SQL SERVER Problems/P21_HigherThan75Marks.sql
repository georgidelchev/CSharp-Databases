CREATE DATABASE HackerRankTests

CREATE TABLE Students
(
    ID    INT PRIMARY KEY IDENTITY,
    Name  NVARCHAR(30) NOT NULL,
    Marks INT          NOT NULL
)

INSERT INTO Students(Name, Marks)
    VALUES ('Ashley', 81),
           ('Samantha', 75),
           ('Julia', 76),
           ('Belvet', 84)

SELECT s.Name
    FROM Students AS s
    WHERE s.Marks > 75
    ORDER BY RIGHT(s.Name, 3), s.ID