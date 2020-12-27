CREATE TABLE Students
(
    ID   INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(30) NOT NULL,

)

CREATE TABLE Friends
(
    ID        INT PRIMARY KEY IDENTITY,
    Friend_ID INT NOT NULL
)

CREATE TABLE Packages
(
    ID     INT PRIMARY KEY IDENTITY,
    Salary FLOAT NOT NULL
)

INSERT INTO Students(Name)
    VALUES ('Ashley'),
           ('Samantha'),
           ('Julia'),
           ('Scarlet')

INSERT INTO Friends(Friend_ID)
    VALUES (2),
           (3),
           (4),
           (1)

INSERT INTO Packages(Salary)
    VALUES (15.20),
           (10.06),
           (11.55),
           (12.12)

SELECT s.Name
    FROM Students AS s
             JOIN Friends AS f ON f.ID = s.ID
             JOIN Packages AS p ON p.ID = s.ID
             JOIN Packages p2 ON p2.ID = f.Friend_ID
    WHERE p.Salary < p2.Salary
    ORDER BY p2.Salary





