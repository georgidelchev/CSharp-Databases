CREATE DATABASE Hotel

CREATE TABLE Employees
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Title NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers
(
	AccountNumber INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	PhoneNumber NVARCHAR(20) NOT NULL,
	EmergencyName NVARCHAR(20) NOT NULL,
	EmergencyNumber NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus
(
	RoomStatus NVARCHAR(50) PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes
(
	RoomType NVARCHAR(50) PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes
(
	BedType NVARCHAR(50) PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms
(
	RoomNumber INT PRIMARY KEY,
	RoomType NVARCHAR(50) NOT NULL,
	BedType NVARCHAR(50) NOT NULL,
	Rate DECIMAL(4, 2),
	RoomStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)

	CONSTRAINT FK_Rooms_RoomType
		FOREIGN KEY (RoomType)
			REFERENCES RoomTypes (RoomType),

	CONSTRAINT FK_Rooms_BedType
		FOREIGN KEY (BedType)
			REFERENCES BedTypes (BedType),

	CONSTRAINT FK_Rooms_RoomStatus
		FOREIGN KEY (RoomStatus)
			REFERENCES RoomStatus (RoomStatus)
)

CREATE TABLE Payments 
(
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	PaymentDate DATE NOT NULL,
	AccountNumber INT NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
	AmountCharged DECIMAL(7, 2) NOT NULL,
	TaxRate DECIMAL(7, 2),
	TaxAmount DECIMAL(7, 2),
	PaymentTotal DECIMAL(7, 2),
	Notes NVARCHAR(MAX)

	CONSTRAINT FK_Payments_EmployeeId
		FOREIGN KEY (EmployeeId)
			REFERENCES Employees (Id),

	CONSTRAINT FK_Payments_AccountNumber
		FOREIGN KEY (AccountNumber)
			REFERENCES Customers (AccountNumber)
)

CREATE TABLE Occupancies
(
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	DateOccupied DATE NOT NULL,
	AccountNumber INT NOT NULL,
	RoomNumber INT NOT NULL,
	RateApplied DECIMAL(7, 2) NOT NULL,
	PhoneCharge DECIMAL(7, 2),
	Notes NVARCHAR(MAX)

	CONSTRAINT FK_Occupancies_EmployeeId
		FOREIGN KEY (EmployeeId)
			REFERENCES Employees (Id),

	CONSTRAINT FK_Occupancies_AccountNumber
		FOREIGN KEY (AccountNumber)
			REFERENCES Customers (AccountNumber),

	CONSTRAINT FK_Occupancies_RoomNumber
		FOREIGN KEY (RoomNumber)
			REFERENCES Rooms (RoomNumber)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
	VALUES ('Ivan','Ivanov','Receptionist','Good Guy'),
		   ('Dimitrichko','Dimitrichev','Cleaner','Funny Guy'),
		   ('Stavri','Stavrev','Boss','Rich Guy')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
	VALUES ('Ivan1', 'Ivanov1', '+123312312311', 'Dimitar1', '+1353462641', 'Rich Guy1'),
		   ('Ivan2', 'Ivanov2', '+123312312312', 'Dimitar2', '+1353462642', 'Rich Guy2'),
		   ('Ivan3', 'Ivanov3', '+123312312313', 'Dimitar3', '+1353462643', 'Rich Guy3')

INSERT INTO RoomStatus (RoomStatus, Notes)
	VALUES ('Dirty', 'Must be cleaned!'),
	       ('Clean', 'Nothing to do.'),
	       ('Very Dirty', 'Must be cleaned soon as possible!!')

INSERT INTO RoomTypes (RoomType, Notes)
	VALUES ('Apartment', 'have fridge'),
		   ('Small room', 'doesn''t have fridge'),
		   ('President Apartment', 'have fridge and air conditioner')

INSERT INTO BedTypes (BedType, Notes)
	VALUES ('Small Bed', '1.20 x 1.40'),
		   ('Medium Bed', '1.40 x 1.60'),
		   ('Big Bed', '1.50 x 2.00')

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes)
	VALUES (1356, 'Apartment', 'Medium Bed', 12.56, 'Clean', 'Cleaned after party.'),
		   (1256, 'Small room', 'Small Bed', 11.56, 'Clean', 'Cleaned after party.'),
		   (5356, 'President Apartment', 'Big Bed', 12.56, 'Dirty', 'Dirty after party.')

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate, TaxAmount, PaymentTotal)
	VALUES (1, '2020-12-01', 1, '2020-12-01', '2020-12-15', 10512.56, 10615.65, 10625.65, 61261.65),
		   (2, '2020-12-01', 2, '2020-12-01', '2020-12-15', 10512.56, 10615.65, 10625.65, 61261.65),
		   (3, '2020-12-01', 3, '2020-12-01', '2020-12-15', 10512.56, 10615.65, 10625.65, 61261.65)

INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes)
	VALUES (1, '2020-12-15', 1, 1256, 10645.65, 10512.67, 'some note'),
		   (2, '2020-12-15', 2, 1356, 10645.65, 10512.67, 'some note'),
		   (3, '2020-12-15', 3, 5356, 10645.65, 10512.67, 'some note')

SELECT * 
	FROM Employees
SELECT * 
	FROM Customers
SELECT * 
	FROM RoomStatus
SELECT * 
	FROM RoomTypes
SELECT * 
	FROM BedTypes
SELECT * 
	FROM Rooms
SELECT * 
	FROM Payments
SELECT * 
	FROM Occupancies
