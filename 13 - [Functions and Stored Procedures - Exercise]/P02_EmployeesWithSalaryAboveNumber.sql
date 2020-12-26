CREATE OR
ALTER PROCEDURE usp_EmployeesWithSalaryAboveNumber(@number DECIMAL(18, 4))
AS
SELECT e.FirstName, e.LastName
    FROM Employees AS e
    WHERE e.Salary >= @number
GO

EXEC usp_EmployeesWithSalaryAboveNumber 48100