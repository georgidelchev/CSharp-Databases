CREATE DATABASE CarRental

CREATE TABLE Categories
(
	Id INT IDENTITY PRIMARY KEY,
	CategoryName NVARCHAR(20) NOT NULL,
	DailyRate DECIMAL(7, 2) NOT NULL,
	WeeklyRate DECIMAL(7, 2) NOT NULL,
	MontlyRate DECIMAL(7, 2) NOT NULL,
	WeekendRate DECIMAL(7, 2) NOT NULL
)

CREATE TABLE Cars
(
	Id INT IDENTITY PRIMARY KEY,
	PlateNumber NVARCHAR(10) NOT NULL,
	Manufacturer NVARCHAR(20) NOT NULL,
	Model NVARCHAR(20) NOT NULL,
	CarYear DATE NOT NULL,
	CategoryId INT NOT NULL,
	Doors TINYINT NOT NULL,
	Picture VARBINARY(MAX) NOT NULL,
	Condition NVARCHAR(MAX) NOT NULL,
	Available CHAR(1) NOT NULL

	CONSTRAINT FK_Cars_CategoryId
		FOREIGN KEY (CategoryId)
			REFERENCES Categories (Id)
)

CREATE TABLE Employees
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(10) NOT NULL,
	LastName NVARCHAR(10) NOT NULL,
	Title NVARCHAR(10) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers
(
	Id INT IDENTITY PRIMARY KEY,
	DriverLicenceNumber NVARCHAR(30) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	City NVARCHAR(20) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders
(
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	CustomerId INT NOT NULL,
	CarId INT NOT NULL,
	TankLevel INT NOT NULL,
	KilometrageStart INT NOT NULL,
	KilometrageEnd INT NOT NULL,
	TotalKilometrage AS KilometrageEnd - KilometrageStart,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
	RateApplied DECIMAL(7, 2) NOT NULL,
	TaxRate DECIMAL(7, 2) NOT NULL,
	OrderStatus NVARCHAR(MAX) NOT NULL,
	Notes NVARCHAR(MAX)

	CONSTRAINT FK_RentalOrders_EmployeeId
		FOREIGN KEY (EmployeeId)
			REFERENCES Employees (Id),

	CONSTRAINT FK_RentalOrders_CustomerId
		FOREIGN KEY (CustomerId)
			REFERENCES Customers (Id),

	CONSTRAINT FK_RentalOrders_CarId
		FOREIGN KEY (CarId)
			REFERENCES Cars (Id)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MontlyRate, WeekendRate)
	VALUES ('Sports1', 10203.56, 10602.12, 50402.65, 90000.12),
	       ('Sports2', 10203.56, 10602.12, 50402.65, 90000.12),
	       ('Sports3', 10203.56, 10602.12, 50402.65, 90000.12)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
	VALUES ('CT 1234 GB', 'Audi', 'RS9', '2020-01-01', 2, 5, 5010, 'NEW', 'Y'),
	       ('CT 2345 GB', 'Audi', 'RS9', '2020-12-01', 3, 2, 6010, 'NEW', 'N'),
	       ('CT 3456 GB', 'Audi', 'RS9', '2020-05-03', 1, 5, 2010, 'NEW', 'Y')

INSERT INTO Employees(FirstName, LastName, Title, Notes)
	VALUES ('DiMitko', 'DiMitev', 'Programmer', 'Hello, its me Dimitrichko!'),
		   ('Ivan', 'Ivanov', 'Salesman', 'Hello, its me Ivan!'),
		   ('Pesho', 'Peshev', 'Cleaner', 'Hello, its me Pesho!')

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes)
	VALUES ('1561A', 'Dimitrichko Dimitrichev', 'Bv. 315', 'Sofia', 55555, 'Killer'),
		   ('15161C', 'Ivan Ivanov', 'Bv. 31155', 'Burgas', 51255, 'Assassin'),
		   ('15651B', 'Stavri Stavrev', 'Bv. 317675', 'Plovdiv', 593555, 'Useless')

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate, RateApplied, TaxRate, OrderStatus)
	VALUES (1, 1, 1, 125, 104512, 670123, '2015-05-12', '2018-07-10', 10723.65, 85126.56, 'Delivered'),
		   (2, 2, 2, 121, 20000, 723721, '2013-05-12', '2020-07-10', 10123.65, 81126.56, 'Waiting'),
		   (3, 3, 3, 165, 126461, 73724, '2012-05-12', '2019-07-10', 10823.65, 85926.56, 'Paid')

SELECT * 
	FROM Categories
SELECT * 
	FROM Cars		   
SELECT * 
	FROM Employees
SELECT * 
	FROM Customers
SELECT * 
	FROM RentalOrders
