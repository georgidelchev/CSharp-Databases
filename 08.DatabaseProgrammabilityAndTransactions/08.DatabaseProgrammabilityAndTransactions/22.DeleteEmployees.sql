CREATE TABLE Deleted_Employees (
    EmployeeId INT NOT NULL IDENTITY(1, 1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    MiddleName NVARCHAR(50),
    JobTitle NVARCHAR(50),
    DepartmentId INT,
    Salary DECIMAL(18, 2),

    CONSTRAINT PK_Deleted_Employees PRIMARY KEY (EmployeeId)
)

CREATE TRIGGER tr_DeleteEmployees 
ON Employees
AFTER DELETE
AS
BEGIN
    INSERT INTO Deleted_Employees (FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary)
         SELECT d.FirstName,
                d.LastName,
                d.MiddleName,
                d.JobTitle,
                d.DepartmentID,
                d.Salary
           FROM DELETED d
END