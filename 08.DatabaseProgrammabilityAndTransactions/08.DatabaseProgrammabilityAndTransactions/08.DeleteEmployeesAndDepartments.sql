CREATE PROC usp_DeleteEmployeesFromDepartment (@DepartmentId INT)
AS
BEGIN
    DECLARE @EmployeesIdToDelete TABLE (Id INT)

    INSERT INTO @EmployeesIdToDelete
         SELECT e.EmployeeID
           FROM Employees e
          WHERE e.DepartmentID = @DepartmentId

     ALTER TABLE Departments
    ALTER COLUMN ManagerID INT NULL
    
    DELETE FROM EmployeesProjects
          WHERE EmployeeID IN (SELECT Id FROM @EmployeesIdToDelete)

    UPDATE Employees
       SET ManagerID = NULL
     WHERE ManagerID IN (SELECT Id FROM @EmployeesIdToDelete)

    UPDATE Departments
       SET ManagerId = NULL
     WHERE ManagerID IN (SELECT Id FROM @EmployeesIdToDelete)

    DELETE FROM Employees
          WHERE EmployeeID IN (SELECT Id FROM @EmployeesIdToDelete)

    DELETE FROM Departments
          WHERE DepartmentID = @DepartmentId

        SELECT COUNT(*) AS [Employees Count] FROM Employees AS e
    INNER JOIN Departments AS d
            ON d.DepartmentID = e.DepartmentID
         WHERE e.DepartmentID = @DepartmentId
END