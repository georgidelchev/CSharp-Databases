CREATE DATABASE Hotel
        COLLATE Cyrillic_General_100_CI_AI

USE Hotel

CREATE TABLE Employees (
    Id INT IDENTITY(1, 1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Title NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_Employees
    PRIMARY KEY (Id)
)

CREATE TABLE Customers (
    AccountNumber INT IDENTITY(1, 1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    EmergencyName NVARCHAR(50),
    EmergencyNumber NVARCHAR(20) NOT NULL,
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_Customers
    PRIMARY KEY (AccountNumber)
)

CREATE TABLE RoomStatus (
    RoomStatus NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_RoomStatus
    PRIMARY KEY (RoomStatus)
)

CREATE TABLE RoomTypes (
    RoomType NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_RoomTypes
    PRIMARY KEY (RoomType)
)

CREATE TABLE BedTypes (
    BedType NVARCHAR(50)NOT NULL,
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_BedTypes
    PRIMARY KEY (BedType)
)

CREATE TABLE Rooms (
    RoomNumber INT IDENTITY(1, 1),
    RoomType NVARCHAR(50),
    BedType NVARCHAR(50),
    Rate DECIMAL(9, 2),
    RoomStatus NVARCHAR(50),
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_Rooms
    PRIMARY KEY (RoomNumber),

    CONSTRAINT FK_Rooms_RoomTypes
    FOREIGN KEY (RoomType)
    REFERENCES RoomTypes(RoomType),

    CONSTRAINT FK_Rooms_BedTypes
    FOREIGN KEY (BedType)
    REFERENCES BedTypes(BedType),

    CONSTRAINT FK_Rooms_RoomStatus
    FOREIGN KEY (RoomStatus)
    REFERENCES RoomStatus(RoomStatus)
)

CREATE TABLE Payments (
    Id INT IDENTITY(1, 1),
    EmployeeId INT,
    PaymentDate DATE NOT NULL,
    AccountNumber INT,
    FirstDateOccupied DATE NOT NULL,
    LastDateOccupied DATE NOT NULL,
    TotalDays AS DATEDIFF(Day, FirstDateOccupied, LastDateOccupied),
    AmountCharged DECIMAL(9, 2) NOT NULL,
    TaxRate DECIMAL(9, 2),
    TaxAmount DECIMAL(9, 2),
    PaymentTotal DECIMAL(9, 2),
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_Payments
    PRIMARY KEY (Id),

    CONSTRAINT FK_Payments_Employees
    FOREIGN KEY (EmployeeId)
    REFERENCES Employees(Id),

    CONSTRAINT FK_Payments_Customers
    FOREIGN KEY (AccountNumber)
    REFERENCES Customers(AccountNumber)
)

CREATE TABLE Occupancies (
    Id INT IDENTITY(1, 1),
    EmployeeId INT,
    DateOccupied DATE,
    AccountNumber INT,
    RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
    RateApplied DECIMAL(9, 2) NOT NULL,
    PhoneCharge DECIMAL(9, 2),
    Notes NVARCHAR(MAX),

    CONSTRAINT PK_Occupancies
    PRIMARY KEY (Id),

    CONSTRAINT FK_Occupancies_Employees
    FOREIGN KEY (EmployeeId)
    REFERENCES Employees(Id),

    CONSTRAINT FK_Occupancies_Customers
    FOREIGN KEY (AccountNumber)
    REFERENCES Customers(AccountNumber),

    CONSTRAINT FK_Occupancies_Rooms
    FOREIGN KEY (RoomNumber)
    REFERENCES Rooms(RoomNumber)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes) 
     VALUES ('Ivan', 'Ivanov', 'Receptionist', 'I am Ivan'),
            ('Martin', 'Martinov', 'Technical support', 'I am Martin'),
            ('Mara', 'Mareva', 'Cleaner', 'I am Marcheto')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyNumber)
     VALUES ('Pesho', 'Peshov', '+395883333333', '123'),
            ('Gosho', 'Goshov', '+395882222222', '123'),
            ('Kosio', 'Kosiov', '+395888888888', '123')

INSERT INTO RoomStatus (RoomStatus, Notes) 
     VALUES ('Clean', 'The room is clean.'),
            ('Dirty', 'The room is dirty.'),
            ('Repair', 'The room is for repair.')

INSERT INTO RoomTypes (RoomType, Notes)
     VALUES ('Small', 'Room with one bed'),
            ('Medium', 'Room with two beds'),
            ('Large', 'Room with three beds')

INSERT INTO BedTypes (BedType)
     VALUES ('Normal'),
            ('Comfort'),
            ('VIP')

INSERT INTO Rooms (RoomType, BedType, Rate, RoomStatus)
     VALUES ('Small', 'Normal', 50, 'Dirty'),
            ('Medium', 'Comfort', 70, 'Clean'),
            ('Large', 'VIP', 100, 'Repair')

INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate)
     VALUES (1, '2015-05-06', 1, '2015-06-18', '2015-07-03', 1256.33, 166.23),
            (2, '2017-10-11', 2, '2017-10-12', '2017-10-22', 556, 125.95),
            (3, '2017-10-26', 3, '2017-11-09', '2017-11-11', 146.74, 100)

INSERT INTO Occupancies (EmployeeId, AccountNumber, RoomNumber, RateApplied)
     VALUES (1, 1, 1, 55.55),
            (2, 2, 2, 44.44),
            (3, 3, 3, 33.33)