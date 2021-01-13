CREATE DATABASE Airport
USE Airport

CREATE TABLE Planes
(
    Id    INT PRIMARY KEY IDENTITY,
    Name  NVARCHAR(30) NOT NULL,
    Seats INT          NOT NULL,
    Range INT          NOT NULL
)

CREATE TABLE Flights
(
    Id            INT PRIMARY KEY IDENTITY,
    DepartureTime DATETIME,
    ArrivalTime   DATETIME,
    Origin        NVARCHAR(50) NOT NULL,
    Destination   NVARCHAR(50) NOT NULL,
    PlaneId       INT          NOT NULL,

    CONSTRAINT FK_Flights_Planes
        FOREIGN KEY (PlaneId)
            REFERENCES Planes (Id)
)

CREATE TABLE Passengers
(
    Id         INT PRIMARY KEY IDENTITY,
    FirstName  NVARCHAR(30) NOT NULL,
    LastName   NVARCHAR(30) NOT NULL,
    Age        INT          NOT NULL,
    Address    NVARCHAR(30) NOT NULL,
    PassportId CHAR(11)     NOT NULL
)

CREATE TABLE LuggageTypes
(
    Id   INT PRIMARY KEY IDENTITY,
    Type NVARCHAR(30) NOT NULL
)

CREATE TABLE Luggages
(
    Id            INT PRIMARY KEY IDENTITY,
    LuggageTypeId INT NOT NULL,
    PassengerId   INT NOT NULL,

    CONSTRAINT FK_Luggages_LuggageTypes
        FOREIGN KEY (LuggageTypeId)
            REFERENCES LuggageTypes (Id),

    CONSTRAINT FK_Luggages_Passengers
        FOREIGN KEY (PassengerId)
            REFERENCES Passengers (Id)
)

CREATE TABLE Tickets
(
    Id          INT PRIMARY KEY IDENTITY,
    PassengerId INT            NOT NULL,
    FlightId    INT            NOT NULL,
    LuggageId   INT            NOT NULL,
    Price       DECIMAL(18, 4) NOT NULL,

    CONSTRAINT FK_Tickets_Passengers
        FOREIGN KEY (PassengerId)
            REFERENCES Passengers (Id),

    CONSTRAINT FK_Tickets_Flights
        FOREIGN KEY (FlightId)
            REFERENCES Flights (Id),

    CONSTRAINT FK_Tickets_Luggages
        FOREIGN KEY (LuggageId)
            REFERENCES Luggages (Id)
)