CREATE TABLE Users (
    Id INT NOT NULL IDENTITY(1, 1),
    Username NVARCHAR(30) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Name NVARCHAR(50),
    Gender CHAR(1) NOT NULL,
    BirthDate DATETIME,
    Age INT,
    Email NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Users PRIMARY KEY    (Id),
    CONSTRAINT UQ_Users_Username UNIQUE (Username),
    CONSTRAINT CH_Users_Gender CHECK (Gender IN ('M', 'F'))
)

CREATE TABLE Departments (
    Id INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Departments PRIMARY KEY (Id)
)

CREATE TABLE Categories (
    Id int NOT NULL IDENTITY(1, 1),
    Name VARCHAR(50) NOT NULL,
    DepartmentId INT,

    CONSTRAINT PK_Categories PRIMARY KEY (Id),
    CONSTRAINT FK_Categories_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
)

CREATE TABLE Status (
    Id INT NOT NULL IDENTITY(1, 1),
    Label VARCHAR(30) NOT NULL,

    CONSTRAINT PK_Status PRIMARY KEY (Id)
)

CREATE TABLE Employees (
    Id INT NOT NULL IDENTITY(1, 1),
    FirstName NVARCHAR(25),
    LastName NVARCHAR(25),
    Gender CHAR(1) NOT NULL,
    BirthDate DATETIME,
    Age INT,
    DepartmentId INT NOT NULL,
    
    CONSTRAINT PK_Employees PRIMARY KEY (Id),
    CONSTRAINT CH_Employees_Gender CHECK (Gender IN ('M', 'F')),
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
)

CREATE TABLE Reports (
    Id INT NOT NULL IDENTITY(1, 1),
    CategoryId INT NOT NULL,
    StatusId INT NOT NULL,
    OpenDate DATETIME NOT NULL,
    CloseDate DATETIME,
    Description VARCHAR(200),
    UserId INT NOT NULL,
    EmployeeId INT,

    CONSTRAINT PK_Reports PRIMARY KEY (Id),
    CONSTRAINT FK_Reports_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    CONSTRAINT FK_Reports_Status FOREIGN KEY (StatusId) REFERENCES Status(Id),
    CONSTRAINT FK_Reports_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Reports_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
)