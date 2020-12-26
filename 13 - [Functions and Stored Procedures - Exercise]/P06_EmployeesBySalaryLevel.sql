CREATE OR
ALTER PROCEDURE usp_EmployeesBySalaryLevel(@levelOfSalary NVARCHAR(MAX))
AS
SELECT e.FirstName,
       e.LastName
    FROM Employees AS e
    WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @levelOfSalary
GO

EXEC usp_EmployeesBySalaryLevel 'high'