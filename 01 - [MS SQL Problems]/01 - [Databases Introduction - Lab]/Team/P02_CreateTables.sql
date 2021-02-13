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