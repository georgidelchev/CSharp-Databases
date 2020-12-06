CREATE DATABASE School

USE School

CREATE TABLE Mountains
(
    Id     INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
)

CREATE TABLE Peaks
(
    Id         INT IDENTITY PRIMARY KEY,
    [Name]     NVARCHAR(100) NOT NULL,
    MountainId INT           NOT NULL,

    CONSTRAINT FK_Peaks_Mountains
        FOREIGN KEY (MountainId)
            REFERENCES Mountains (Id)
)

INSERT INTO Mountains([Name])
    VALUES ('Rila'),
           ('Pirin'),
           ('Vitosha'),
           ('Stara Planina')

INSERT INTO Peaks(Name, MountainId)
    VALUES ('Musala', 1),
           ('Cherni Vruh', 3),
           ('K2', 4)

SELECT *
    FROM Mountains

SELECT *
    FROM Peaks

SELECT m.Name, p.Name
    FROM Mountains AS m
             JOIN Peaks AS p ON m.Id = p.MountainId

USE Geography
SELECT *
    FROM Peaks

SELECT m.MountainRange, p.PeakName, p.Elevation
    FROM Peaks AS p
             JOIN Mountains AS m ON p.MountainId = m.Id
    WHERE m.MountainRange = 'Rila'
    ORDER BY Elevation DESC





