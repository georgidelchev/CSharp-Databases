CREATE PROC usp_EmployeesBySalaryLevel (@SalaryLevel NVARCHAR(50))
AS
BEGIN
    SELECT e.FirstName,
           e.LastName
      FROM Employees e
     WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @SalaryLevel 
END