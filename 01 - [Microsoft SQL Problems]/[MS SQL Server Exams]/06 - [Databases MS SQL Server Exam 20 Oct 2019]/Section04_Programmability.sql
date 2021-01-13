-- 11. Hours to Complete
CREATE OR
ALTER FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
    RETURNS INT
AS
BEGIN
    DECLARE @result INT = 0

    IF (@StartDate IS NOT NULL AND @EndDate IS NOT NULL)
        BEGIN
            SET @result = DATEDIFF(HOUR, @StartDate, @EndDate)
        END

    RETURN @result
END
GO

SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours
    FROM Reports

-- 12. Assign Employee
CREATE OR
ALTER PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
DECLARE
    @employee INT = (SELECT e.DepartmentId
                         FROM Employees AS e
                         WHERE e.Id = @EmployeeId)

DECLARE
    @report INT = (SELECT C.DepartmentId
                       FROM Reports AS r
                                JOIN Categories C ON r.CategoryId = C.Id
                       WHERE r.Id = @ReportId)
    IF (@employee != @report)
        THROW 100000,'Employee doesn''t belong to the appropriate department!',1

UPDATE Reports
SET EmployeeId=@EmployeeId
    WHERE Id = @ReportId
GO

EXEC usp_AssignEmployeeToReport 30, 1
-- Employee doesn't belong to the appropriate department!

EXEC usp_AssignEmployeeToReport 17, 2
-- (1 row affected)
