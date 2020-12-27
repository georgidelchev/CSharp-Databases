CREATE TABLE Employees
(
    Id     INT PRIMARY KEY IDENTITY,
    Name   NVARCHAR(30) NOT NULL,
    Salary INT          NOT NULL
)

INSERT INTO Employees(Name, Salary)
    VALUES ('Kristeen', 1420),
           ('Ashley', 2006),
           ('Julia', 2210),
           ('Maria', 3000)

SELECT CAST(CEILING(AVG(Salary * 1.0) - AVG(CAST(REPLACE(Salary, 0, '') AS FLOAT))) AS INT)
    FROM Employees