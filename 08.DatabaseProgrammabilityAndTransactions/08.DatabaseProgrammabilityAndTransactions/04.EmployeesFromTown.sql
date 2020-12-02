CREATE PROC usp_GetEmployeesFromTown (@TownName NVARCHAR(50))
AS
BEGIN
        SELECT e.FirstName,
               e.LastName
          FROM Employees e
    INNER JOIN Addresses a
            ON a.AddressID = e.AddressID
    INNER JOIN Towns t
            ON t.TownID = a.TownID
         WHERE t.Name = @TownName
END