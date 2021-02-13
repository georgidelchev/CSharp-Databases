CREATE DATABASE Service
USE Service

CREATE TABLE Users
(
    Id        INT PRIMARY KEY IDENTITY,
    Username  NVARCHAR(30) UNIQUE NOT NULL,
    Password  NVARCHAR(50)        NOT NULL,
    Name      NVARCHAR(50),
    Birthdate DATETIME,
    Age       INT
        CHECK (Age >= 14 AND Age <= 110),
    Email     NVARCHAR(50)        NOT NULL
)

CREATE TABLE Departments
(
    Id   INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
    Id           INT PRIMARY KEY IDENTITY,
    FirstName    NVARCHAR(25),
    LastName     NVARCHAR(25),
    Birthdate    DATETIME,
    Age          INT
        CHECK (Age >= 18 AND Age <= 110),
    DepartmentId INT,

    CONSTRAINT FK_Employees_Departments
        FOREIGN KEY (DepartmentId)
            REFERENCES Departments (Id)
)

CREATE TABLE Categories
(
    Id           INT PRIMARY KEY IDENTITY,
    Name         NVARCHAR(50) NOT NULL,
    DepartmentId INT          NOT NULL,

    CONSTRAINT FK_Categories_Departments
        FOREIGN KEY (DepartmentId)
            REFERENCES Departments (Id)
)

CREATE TABLE Status
(
    Id    INT PRIMARY KEY IDENTITY,
    Label NVARCHAR(30) NOT NULL
)

CREATE TABLE Reports
(
    Id          INT PRIMARY KEY IDENTITY,
    CategoryId  INT           NOT NULL,
    StatusId    INT           NOT NULL,
    OpenDate    DATETIME      NOT NULL,
    CloseDate   DATETIME,
    Description NVARCHAR(200) NOT NULL,
    UserId      INT           NOT NULL,
    EmployeeId  INT,

    CONSTRAINT FK_Reports_Categories
        FOREIGN KEY (CategoryId)
            REFERENCES Categories (Id),

    CONSTRAINT FK_Reports_Status
        FOREIGN KEY (StatusId)
            REFERENCES Status (Id),

    CONSTRAINT FK_Reports_Users
        FOREIGN KEY (UserId)
            REFERENCES Users (Id),

    CONSTRAINT FK_Reports_Employees
        FOREIGN KEY (EmployeeId)
            REFERENCES Employees (Id),
)
