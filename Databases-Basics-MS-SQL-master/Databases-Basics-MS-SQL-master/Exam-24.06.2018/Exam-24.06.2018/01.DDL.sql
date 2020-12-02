CREATE TABLE Cities (
    Id INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(20) NOT NULL,
    CountryCode CHAR(2) NOT NULL,

    CONSTRAINT PK_Cities PRIMARY KEY (Id)
)

CREATE TABLE Hotels (
    Id INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(30) NOT NULL,
    CityId INT NOT NULL,
    EmployeeCount INT NOT NULL,
    BaseRate DECIMAL(18, 2),

    CONSTRAINT PK_Hotels PRIMARY KEY (Id),
    CONSTRAINT FK_Hotels_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id)
)

CREATE TABLE Rooms (
    Id INT NOT NULL IDENTITY(1, 1),
    Price DECIMAL(18, 2) NOT NULL,
    Type NVARCHAR(20) NOT NULL,
    Beds INT NOT NULL,
    HotelId INT NOT NULL,

    CONSTRAINT PK_Rooms PRIMARY KEY (Id),
    CONSTRAINT FK_Rooms_Hotels FOREIGN KEY (HotelId) REFERENCES Hotels(Id)
)

CREATE TABLE Trips (
    Id INT NOT NULL IDENTITY(1, 1),
    RoomId INT NOT NULL,
    BookDate DATE NOT NULL,
    ArrivalDate DATE NOT NULL,
    ReturnDate DATE NOT NULL,
    CancelDate DATE,

    CONSTRAINT PK_Trips PRIMARY KEY (Id),
    CONSTRAINT FK_Trips_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id),
    CONSTRAINT CH_Trips_ArrivalDate CHECK (BookDate < ArrivalDate),
    CONSTRAINT CH_Trips_ReturnDate CHECK (ArrivalDate < ReturnDate)
)

CREATE TABLE Accounts (
    Id INT NOT NULL IDENTITY(1, 1),
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(20),
    LastName NVARCHAR(50) NOT NULL,
    CityId INT NOT NULL,
    BirthDate date NOT NULL,
    Email VARCHAR(100) NOT NULL,

    CONSTRAINT PK_Accounts PRIMARY KEY (Id),
    CONSTRAINT FK_Accounts_Cities FOREIGN KEY (CityId) REFERENCES Cities(Id),
    CONSTRAINT UQ_Accounts_Email UNIQUE (Email)
)

CREATE TABLE AccountsTrips (
    AccountId INT NOT NULL,
    TripId INT NOT NULL,
    Luggage INT NOT NULL,

    CONSTRAINT PK_AccountsTrips PRIMARY KEY (AccountId, TripId),
    CONSTRAINT FK_AccountsTrips_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(Id),
    CONSTRAINT FK_AccountsTrips_Trips FOREIGN KEY (TripId) REFERENCES Trips(Id),
    CONSTRAINT CH_AccountsTrips_Luggage CHECK (Luggage >= 0)
)