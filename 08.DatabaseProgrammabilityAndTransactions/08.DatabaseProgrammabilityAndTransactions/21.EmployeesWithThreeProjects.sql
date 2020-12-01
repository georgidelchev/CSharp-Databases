CREATE PROC usp_AssignProject(@EmployeeId INT, @ProjectID INT)
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @EmployeeProjects INT
    SET @EmployeeProjects = (SELECT COUNT(ep.ProjectID)
                               FROM EmployeesProjects ep
                              WHERE ep.EmployeeID = @EmployeeId)
    IF (@EmployeeProjects >= 3)
    BEGIN
        RAISERROR('The employee has too many projects!', 16, 1)
        ROLLBACK
        RETURN
    END

    INSERT INTO EmployeesProjects (EmployeeID, ProjectID)
         VALUES (@EmployeeId, @ProjectID)
    COMMIT
END