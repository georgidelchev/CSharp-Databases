CREATE PROC usp_AssignEmployeeToReport (@EmployeeId INT, @ReportId INT)
AS
BEGIN
    DECLARE @CategoryId INT
    SET @CategoryId = (SELECT r.CategoryId FROM Reports r WHERE r.Id = @ReportId)

    DECLARE @DepartmentEmployeeId INT
    SET @DepartmentEmployeeId = (SELECT e.DepartmentId FROM Employees e WHERE e.Id = @EmployeeId)

    DECLARE @DepartmentCategoryId INT
    SET @DepartmentCategoryId = (SELECT c.DepartmentId FROM Categories c WHERE c.Id = @CategoryId)

    BEGIN TRAN
        UPDATE Reports
           SET EmployeeId = @EmployeeId
         WHERE Id = @ReportId

         IF (@DepartmentEmployeeId <> @DepartmentCategoryId)
        BEGIN
            ROLLBACK
            RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1)
            RETURN
        END
    COMMIT
END