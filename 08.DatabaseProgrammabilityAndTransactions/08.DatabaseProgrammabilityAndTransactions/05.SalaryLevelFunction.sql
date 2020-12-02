CREATE FUNCTION ufn_GetSalaryLevel (@Salary DECIMAL(18,4))
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @SalaryLevel NVARCHAR(50)
    SET @SalaryLevel =
    CASE 
        WHEN @Salary < 30000 THEN 'Low'
        WHEN @Salary BETWEEN 30000 AND 50000 THEN 'Average'
        ELSE 'High'
    END

    RETURN @SalaryLevel
END