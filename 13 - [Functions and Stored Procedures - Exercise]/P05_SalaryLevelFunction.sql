CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18, 4))
    RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @result NVARCHAR(MAX)

    IF (@salary < 30000)
        SET @result = 'Low'
    ELSE
        IF (@salary BETWEEN 30000 AND 50000)
            SET @result = 'Average'
        ELSE
            SET @result = 'High'

    RETURN @result
END
GO

SELECT e.Salary,
       dbo.ufn_GetSalaryLevel(e.Salary) AS SalaryLevel
    FROM Employees AS e
