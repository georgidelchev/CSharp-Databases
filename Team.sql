CREATE DATABASE Team;

CREATE TABLE Players
(
	Player_ID int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Player_FirstName nvarchar(50) NOT NULL,
	Player_LastName nvarchar(50) NOT NULL,
	Player_Birthdate date NOT NULL,
	Player_Position varchar(20) NOT NULL
)

CREATE TABLE Coaches
(
	Coach_ID int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Coach_FirstName nvarchar(50) NOT NULL,
	Coach_LastName nvarchar(50) NOT NULL,
	Coach_Birthdate date NOT NULL
)

CREATE TABLE Sponsors
(
	Sponsor_ID int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Sponsor_Brand nvarchar(50) NOT NULL,
	Sponsor_Budget decimal(10, 2) NOT NULL
)

ALTER TABLE Players 
	ADD Player_Salary decimal(10, 2);
	
ALTER TABLE Coaches 
	ADD Coach_Salary decimal(10, 2);

INSERT INTO Players(Player_FirstName, Player_LastName, Player_Birthdate, Player_Position, Player_Salary)
	VALUES ('David', 'DeGea', '1990-05-21', 'GK', 100000),
	       ('Sergio', 'Romero', '1990-05-21', 'GK', 150000),
	       ('Victor', 'Lindelof', '1990-05-21', 'DF', 190000),
	       ('Paul', 'Pogba', '1990-05-21', 'MDF', 110000),
	       ('Edison', 'Cavani', '1990-05-21', 'FW', 220000.56),
	       ('Marcus', 'Rashford', '1990-05-21', 'FW', 670000.22);

INSERT INTO Coaches(Coach_FirstName, Coach_LastName, Coach_Birthdate, Coach_Salary)
	VALUES ('Jurgen', 'Klopp', '1990-05-21', 1000600.67),
		   ('Coach', 'Coachoff', '1990-05-21', 150500.56);

INSERT INTO Sponsors(Sponsor_Brand, Sponsor_Budget)
	VALUES ('Nike', 1000000.16),
	       ('Adidas', 2000000.66),
	       ('Porsche', 1050000.26),
	       ('Audi', 4000000.51);

--delete from Players
--	DBCC CHECKIDENT (Players, RESEED, 0)