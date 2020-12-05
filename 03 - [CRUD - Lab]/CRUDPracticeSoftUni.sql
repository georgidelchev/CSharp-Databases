-- Select all from towns
SELECT *
    FROM Towns

-- Get Length of the names
SELECT LEN([Name]) AS NamesLength
    FROM Towns

-- Get first letter of the names
SELECT LEFT([Name], 1) AS FirstLetter
    FROM Towns

SELECT GETDATE() AS CurrentTime

-- Select all from towns with id bigger or equal to 10
SELECT *
    FROM Towns
    WHERE TownID >= 10

-- Select all from towns where name is 'Sofia'
SELECT *
    FROM Towns
    WHERE [Name] = 'Sofia'

-- Select all from towns where first letter is 'S'
SELECT *
    FROM Towns
    WHERE LEFT([Name], 1) = 'S'

SELECT AddressText, [Name]
    FROM Addresses
             JOIN Towns ON Addresses.TownID = Towns.TownID
    WHERE [Name] = 'Sofia'

SELECT [Name] AS [Town Name], LEN([Name]) AS TownLength, LEFT([Name], 1) AS FirstLetter
    FROM Towns

SELECT FirstName + ' ' + LastName AS FullName
    FROM Employees

SELECT FirstName + ' ' + LastName AS FullName, JobTitle, Salary
    FROM Employees

SELECT TOP (10) *
    FROM Employees

-- non-unique job titles
SELECT JobTitle
    FROM Employees

-- unique job titles
SELECT DISTINCT JobTitle
    FROM Employees

SELECT COUNT(DISTINCT JobTitle) AS UniqueJobsCount
    FROM Employees

SELECT JobTitle, COUNT(*) AS EmployeeCount
    FROM Employees
    GROUP BY JobTitle

SELECT JobTitle, MAX(Salary) AS MaxSalary, MIN(Salary) AS MinSalary, COUNT(*) AS EmployeeCount
    FROM Employees
    GROUP BY JobTitle

SELECT *
    FROM Employees
    WHERE Salary >= 40000

SELECT *
    FROM Employees
    WHERE FirstName LIKE '%ris'

SELECT *
    FROM Employees
    WHERE Salary != 10000

SELECT *
    FROM Employees
    WHERE (Salary = 11110000 OR Salary = 50500)

SELECT *
    FROM Employees
    WHERE (Salary >= 10000 AND Salary <= 20000 AND JobTitle = 'Recruiter')

SELECT *
    FROM Employees
    WHERE Salary IN (10000, 20000, 30000, 40000, 50000, 50500)

SELECT *
    FROM Employees
    WHERE Salary NOT IN (10000, 20000, 30000, 40000, 50000, 50500)

SELECT *
    FROM Employees
    WHERE MiddleName IS NULL

SELECT *
    FROM Employees
    ORDER BY Salary

SELECT *
    FROM Employees
    ORDER BY Salary DESC

SELECT *
    FROM Employees
    ORDER BY HireDate DESC

SELECT *
    FROM Employees
    WHERE HireDate >= '2000-01-01'
      AND HireDate < '2001-01-01'

SELECT *
    FROM Employees
    WHERE YEAR(HireDate) = 2000

SELECT *
    FROM Towns

INSERT INTO Towns
    VALUES ('Stara Zagora')

SELECT *
    FROM Employees

INSERT INTO Employees(FirstName, LastName, JobTitle, DepartmentID, HireDate, Salary)
    VALUES ('Dimitar', 'Ivanov', 'Programmer', 5, GETDATE(), 10000),
           ('Fasul', 'Fasulov', 'JuniorProgrammer', 1, GETDATE(), 110000)

SELECT [Name], Description, GETDATE() AS StartDate
    FROM Projects
    WHERE [Name] LIKE 'C%'

SELECT FirstName + ' ' + LastName AS FullName, Salary AS Salary
    INTO Names
    FROM Employees

CREATE SEQUENCE seq_MyNumber
    AS INT START WITH 0 INCREMENT BY 1001

SELECT NEXT VALUE
           FOR seq_MyNumber

UPDATE Names
SET Salary=Salary + 1000

SELECT *
    FROM Names

SELECT *
    FROM Projects
    WHERE EndDate IS NULL

UPDATE Projects
SET EndDate = GETDATE()
    WHERE EndDate IS NULL

SELECT *
    FROM Projects


