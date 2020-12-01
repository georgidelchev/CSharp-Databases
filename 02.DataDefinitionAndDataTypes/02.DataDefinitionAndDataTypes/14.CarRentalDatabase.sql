CREATE DATABASE CarRental
        COLLATE Cyrillic_General_100_CI_AI

USE CarRental

CREATE TABLE Categories (
    Id INT IDENTITY(1, 1),
    CategoryName NVARCHAR(50) NOT NULL,
    DailyRate DECIMAL(9, 2),
    WeekLyRate DECIMAL(9, 2),
    MonthlyRate DECIMAL(9, 2),
    WeekendRate DECIMAL(9, 2),
    
    CONSTRAINT PK_Categories
    PRIMARY KEY (Id)
)

CREATE TABLE Cars (
    Id INT IDENTITY(1, 1),
    PlateNumber NVARCHAR(10) NOT NULL,
    Manufacturer NVARCHAR(20) NOT NULL,
    Model NVARCHAR(20) NOT NULL,
    CarYear INT NOT NULL,
    CategoryId INT,
    Doors INT NOT NULL,
    Picture VARBINARY(MAX),
    Condition NVARCHAR(MAX),
    Available BIT NOT NULL,
    
    CONSTRAINT PK_Cars
    PRIMARY KEY (Id),
    
    CONSTRAINT FK_Cars_Categories
    FOREIGN KEY (CategoryId)
    REFERENCES Categories(Id)
)

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
    Id INT IDENTITY(1, 1),
    DriverLicenseNumber INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Adress NVARCHAR(100) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    ZIPCode INT NOT NULL,
    Notes NVARCHAR(MAX),
    
    CONSTRAINT PK_Customers
    PRIMARY KEY (Id)
)

CREATE TABLE RentalOrders (
    Id INT IDENTITY(1, 1),
    EmployeeId INT,
    CustomerId INT,
    CarId INT FOREIGN KEY REFERENCES Cars(Id),
    TankLevel INT NOT NULL,
    KilometrageStart INT NOT NULL,
    KilometrageEnd INT NOT NULL,
    TotalKilometrage AS KilometrageEnd - KilometrageStart,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
    RateApplied DECIMAL(9, 2),
    TaxRate DECIMAL(9, 2),
    OrderStatus NVARCHAR(50),
    Notes NVARCHAR(MAX),
    
    CONSTRAINT PK_RentalOrders
    PRIMARY KEY (Id),
    
    CONSTRAINT FK_RentalOrders_Employees
    FOREIGN KEY (EmployeeId)
    REFERENCES Employees(Id),
    
    CONSTRAINT FK_RentalOrders_Customers
    FOREIGN KEY (CustomerId)
    REFERENCES Customers(Id),
)

INSERT INTO Categories (CategoryName, DailyRate, WeekLyRate, MonthlyRate, WeekendRate) 
     VALUES ('Car', 20, 120, 500, 42.50),
            ('Bus', 250, 1600, 6000, 489.99),
            ('Truck', 500, 3000, 11900, 949.90)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) 
     VALUES ('123456ABCD', 'Mazda', 'CX-5', 2016, 1, 5, 123456, 'Perfect', 1),
            ('asdafof145', 'Mercedes', 'Tourismo', 2017, 2, 3, 99999, 'Perfect', 1),
            ('asdp230456', 'MAN', 'TGX', 2016, 3, 2, 123456, 'Perfect', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes) 
     VALUES ('Ivan', 'Ivanov', 'Seller', 'I am Ivan'),
            ('Georgi', 'Georgiev', 'Seller', 'I am Gosho'),
            ('Mitko', 'Mitkov', 'Manager', 'I am Mitko')

INSERT INTO Customers (DriverLicenseNumber, FullName, Adress, City, ZIPCode, Notes)
     VALUES (123456789, 'Gogo Gogov', 'ул. Пиротска 5', 'София', 1233, 'Good driver'),
            (347645231, 'Mara Mareva', 'ул. Иван Драсов 14', 'Варна', 5678, 'Bad driver'),
            (123574322, 'Strahil Strahilov', 'ул. Кестен 4', 'Дупница', 5689, 'Good driver')

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate) 
     VALUES (1, 1, 1, 54, 2189, 2456, '2017-11-05', '2017-11-08'),
            (2, 2, 2, 22, 13565, 14258, '2017-11-06', '2017-11-11'),
            (3, 3, 3, 180, 1202, 1964, '2017-11-09', '2017-11-12')