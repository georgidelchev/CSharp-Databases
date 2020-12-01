CREATE DATABASE SoftUni
        COLLATE Cyrillic_General_100_CI_AI

USE SoftUni

CREATE TABLE Towns (
    Id INT IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,
    
    CONSTRAINT PK_Towns
    PRIMARY KEY (Id)
)

CREATE TABLE Addresses (
    Id INT IDENTITY(1, 1),
    AddressText NVARCHAR(50) NOT NULL,
    TownId INT,
    
    CONSTRAINT PK_Addresses
    PRIMARY KEY (Id),
    
    CONSTRAINT FK_Addresses_Towns
    FOREIGN KEY (TownId)
    REFERENCES Towns(Id)
)

CREATE TABLE Departments (
    Id INT IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,
    
    CONSTRAINT PK_Departments
    PRIMARY KEY (Id)
)

CREATE TABLE Employees (
    Id INT IDENTITY(1, 1),
    FirstName NVARCHAR(20) NOT NULL,
    MiddleName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    JobTitle NVARCHAR(20) NOT NULL,
    DepartmentId INT,
    HireDate DATE,
    Salary DECIMAL(9, 2) NOT NULL,
    AddressId INT FOREIGN KEY REFERENCES Addresses(Id),
    
    CONSTRAINT PK_Employees
    PRIMARY KEY (Id),
    
    CONSTRAINT FK_Employees_Departments
    FOREIGN KEY (DepartmentId)
    REFERENCES Departments(Id),
    
    CONSTRAINT FK_Employees_Addresses
    FOREIGN KEY (AddressId)
    REFERENCES Addresses(Id)
)