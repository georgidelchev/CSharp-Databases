CREATE FUNCTION udf_GetReportsCount (@EmployeeId INT, @StatusId INT)
RETURNS INT
AS
BEGIN
    DECLARE @ReportsCount INT
    SET @ReportsCount = (SELECT COUNT(r.Id)
                           FROM Reports r
                          WHERE r.EmployeeId = @EmployeeId AND r.StatusId = @StatusId)
    RETURN @ReportsCount
END