-- First Task
CREATE DATABASE Minions;

-- Second Task
CREATE TABLE Minions
(
	Id int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	Age int NOT NULL 
)

CREATE TABLE Towns
(
	Id int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
)

-- Third Task
ALTER TABLE Minions ADD TownId int NOT NULL
