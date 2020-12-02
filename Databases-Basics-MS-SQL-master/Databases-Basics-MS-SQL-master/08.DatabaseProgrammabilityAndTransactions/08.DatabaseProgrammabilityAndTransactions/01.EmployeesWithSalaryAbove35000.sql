CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
BEGIN
    SELECT e.FirstName,
           e.LastName
      FROM Employees e
     WHERE e.Salary > 35000
END