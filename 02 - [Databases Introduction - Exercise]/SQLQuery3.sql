-- First Task
CREATE DATABASE Minions;

-- Second Task
CREATE TABLE Minions
(
	Id int PRIMARY KEY,
	[Name] nvarchar(50),
	Age int
)

CREATE TABLE Towns
(
	Id int PRIMARY KEY,
	[Name] nvarchar(50),
)

-- Third Task
ALTER TABLE Minions ADD TownId int

ALTER TABLE Minions
	ADD CONSTRAINT FK_Minions_Towns
		FOREIGN KEY (TownId)
			REFERENCES Towns(Id);

-- Forth Task
INSERT INTO Towns
	VALUES (1, 'Sofia'),
		   (2, 'Plovdiv'),
		   (3, 'Varna');

INSERT INTO Minions
	VALUES(1, 'Kevin', 22, 1),
		  (2, 'Kevin', 15, 3),
		  (3, 'Steward', NULL, 2);

-- Fifth Task
TRUNCATE TABLE Minions

-- Sixth Task
DROP TABLE Minions

DROP TABLE Towns