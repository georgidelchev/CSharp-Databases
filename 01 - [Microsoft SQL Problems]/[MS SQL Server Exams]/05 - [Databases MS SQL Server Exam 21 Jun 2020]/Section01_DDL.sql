CREATE DATABASE TripService
USE TripService

CREATE TABLE Cities
(
    Id          INT PRIMARY KEY IDENTITY,
    Name        NVARCHAR(20) NOT NULL,
    CountryCode CHAR(2)      NOT NULL
)

CREATE TABLE Hotels
(
    Id            INT PRIMARY KEY IDENTITY,
    Name          NVARCHAR(30) NOT NULL,
    CityId        INT          NOT NULL,
    EmployeeCount INT          NOT NULL,
    BaseRate      DECIMAL(18, 2),

    CONSTRAINT FK_Hotels_Cities
        FOREIGN KEY (CityId)
            REFERENCES Cities (Id)
)

CREATE TABLE Rooms
(
    Id      INT PRIMARY KEY IDENTITY,
    Price   DECIMAL(18, 2) NOT NULL,
    Type    NVARCHAR(20)   NOT NULL,
    Beds    INT            NOT NULL,
    HotelId INT            NOT NULL,

    CONSTRAINT FK_Rooms_Hotels
        FOREIGN KEY (HotelId)
            REFERENCES Hotels (Id)
)

CREATE TABLE Trips
(
    Id          INT PRIMARY KEY IDENTITY,
    RoomId      INT  NOT NULL,
    BookDate    DATE NOT NULL,
    ArrivalDate DATE NOT NULL,
    ReturnDate  DATE NOT NULL,
    CancelDate  DATE,

    CONSTRAINT CK_BookDate_Before_ArrivalDate
        CHECK (BookDate < ArrivalDate),

    CONSTRAINT CK_ArrivalDate_Before_ReturnDate
        CHECK (ArrivalDate < ReturnDate),

    CONSTRAINT FK_Trips_Rooms
        FOREIGN KEY (RoomId)
            REFERENCES Rooms (Id)
)

CREATE TABLE Accounts
(
    Id         INT PRIMARY KEY IDENTITY,
    FirstName  NVARCHAR(50)  NOT NULL,
    MiddleName NVARCHAR(20),
    LastName   NVARCHAR(50)  NOT NULL,
    CityId     INT           NOT NULL,
    BirthDate  DATE          NOT NULL,
    Email      NVARCHAR(100) NOT NULL UNIQUE,

    CONSTRAINT FK_Accounts_Cities
        FOREIGN KEY (CityId)
            REFERENCES Cities (Id)
)

CREATE TABLE AccountsTrips
(
    AccountId INT NOT NULL,
    TripId    INT NOT NULL,
    Luggage   INT NOT NULL CHECK (Luggage >= 0),

    PRIMARY KEY (AccountId, TripId),

    CONSTRAINT FK_AccountsTrips_Accounts
        FOREIGN KEY (AccountId)
            REFERENCES Accounts (Id),

    CONSTRAINT FK_AccountsTrips_Trips
        FOREIGN KEY (TripId)
            REFERENCES Trips (Id),
)
