CREATE TABLE Clients (
    Id INT NOT NULL IDENTITY(1, 1),
    FirstName NVARCHAR(30) NOT NULL,
    LastName NVARCHAR(30) NOT NULL,
    Gender CHAR(1) NOT NULL,
    BirthDate DATETIME,
    CreditCard NVARCHAR(30) NOT NULL,
    CardValidity DATETIME,
    Email NVARCHAR(50) NOT NULL

    CONSTRAINT PK_Clients PRIMARY KEY (Id),
    CONSTRAINT CH_Clients_Gender CHECK(Gender IN ('M', 'F'))
)

CREATE TABLE Towns (
    Id INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Towns PRIMARY KEY (Id)
)

CREATE TABLE Offices (
    Id INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(40),
    ParkingPlaces INT,
    TownId INT NOT NULL,

    CONSTRAINT PK_Offices PRIMARY KEY (Id),
    CONSTRAINT FK_Offices_Towns FOREIGN KEY (TownId) REFERENCES Towns(Id)
)

CREATE TABLE Models (
    Id INT NOT NULL IDENTITY(1, 1),
    Manufacturer NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    ProductionYear DATETIME,
    Seats INT,
    Class NVARCHAR(10),
    Consumption DECIMAL(14, 2)

    CONSTRAINT PK_Models PRIMARY KEY (Id)
)    

CREATE TABLE Vehicles (
    Id INT NOT NULL IDENTITY(1, 1),
    ModelId INT NOT NULL,
    OfficeId INT NOT NULL,
    Mileage INT,

    CONSTRAINT PK_Vehicles PRIMARY KEY (Id),
    CONSTRAINT FK_Vehicles_Models FOREIGN KEY (ModelId) REFERENCES Models(Id),
    CONSTRAINT FK_Vehicles_Offices FOREIGN KEY (OfficeId) REFERENCES Offices(Id)
)

CREATE TABLE Orders (
    Id INT NOT NULL IDENTITY(1, 1),
    ClientId INT NOT NULL,
    TownId INT NOT NULL,
    VehicleId INT NOT NULL,
    CollectionDate DATETIME NOT NULL,
    CollectionOfficeId INT NOT NULL,
    ReturnDate DATETIME,
    ReturnOfficeId INT,
    Bill DECIMAL(14, 2),
    TotalMileage INT,

    CONSTRAINT PK_Orders PRIMARY KEY (Id),
    CONSTRAINT FK_Orders_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id),
    CONSTRAINT FK_Orders_Towns FOREIGN KEY (TownId) REFERENCES Towns(Id),
    CONSTRAINT FK_Orders_Vehicles FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id),
    CONSTRAINT FK_Orders_CollectionOffices FOREIGN KEY (CollectionOfficeId) REFERENCES Offices(Id),
    CONSTRAINT FK_Orders_ReturnOffices FOREIGN KEY (ReturnOfficeId) REFERENCES Offices(Id)
)