CREATE PROCEDURE usp_GetEmployeesFromTown(@townName NVARCHAR(MAX))
AS
SELECT e.FirstName, e.LastName
    FROM Employees AS e
             JOIN Addresses a ON a.AddressID = e.AddressID
             JOIN Towns t ON a.TownID = t.TownID
    WHERE t.Name = @townName
GO

EXEC usp_GetEmployeesFromTown 'Sofia'
