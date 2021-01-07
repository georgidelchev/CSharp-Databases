CREATE OR
ALTER PROCEDURE usp_DeleteEmployeesFromDepartment(@departmentId int)
AS
DELETE
    FROM EmployeesProjects
    WHERE EmployeeID IN (SELECT EmployeeID
                             FROM Employees
                             WHERE DepartmentID = @departmentId)

UPDATE Employees
SET ManagerID=NULL
    WHERE ManagerID IN (SELECT EmployeeID
                            FROM Employees
                            WHERE DepartmentID = @departmentId)
    ALTER TABLE Departments
        ALTER COLUMN ManagerID int

UPDATE Departments
SET ManagerID=NULL
    WHERE ManagerID IN (SELECT EmployeeID
                            FROM Employees
                            WHERE Employees.DepartmentID = @departmentId)

DELETE
    FROM Employees
    WHERE DepartmentID = @departmentId

DELETE
    FROM Departments
    WHERE DepartmentID = @departmentId

SELECT COUNT(*)
    FROM Employees
    WHERE DepartmentID = @departmentId
GO

usp_DeleteEmployeesFromDepartment 5