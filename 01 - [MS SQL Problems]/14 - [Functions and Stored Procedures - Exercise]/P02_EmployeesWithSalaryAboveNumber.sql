CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@number DECIMAL(18, 4))
AS
SELECT e.FirstName, e.LastName
    FROM Employees AS e
    WHERE e.Salary >= @number
GO

EXEC usp_EmployeesWithSalaryAboveNumber 48100
