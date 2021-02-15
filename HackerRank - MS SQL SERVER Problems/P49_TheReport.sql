CREATE TABLE Students
(
    Id    INT PRIMARY KEY IDENTITY,
    Name  NVARCHAR(30) NOT NULL,
    Marks INT
)

CREATE TABLE Grades
(
    Grade    INT PRIMARY KEY IDENTITY,
    Min_Mark INT,
    Max_Mark INT
)

INSERT INTO Grades(Min_Mark, Max_Mark)
    VALUES (0, 9),
           (10, 19),
           (20, 29),
           (30, 39),
           (40, 49),
           (50, 59),
           (60, 69),
           (70, 79),
           (80, 89),
           (90, 100)

INSERT INTO Students(Name, Marks)
    VALUES ('Julia', 88),
           ('Samantha', 68),
           ('Maria', 99),
           ('Scarlet', 78),
           ('Ashley', 63),
           ('Jane', 81)


SELECT IIF(g.Grade < 8, NULL, s.Name) AS Name1,
       g.Grade,
       s.Marks
    FROM Students AS s
             JOIN Grades AS g ON s.Marks
        BETWEEN g.Min_Mark AND g.Max_Mark
    ORDER BY g.Grade DESC, Name1, s.Marks



