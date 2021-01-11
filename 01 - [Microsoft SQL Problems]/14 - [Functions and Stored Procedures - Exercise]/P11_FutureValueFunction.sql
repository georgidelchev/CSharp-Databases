CREATE OR
ALTER FUNCTION ufn_CalculateFutureValue(@initialSum DECIMAL(18, 4), @yearlyInterestRate FLOAT, @numberOfYears INT)
    RETURNS DECIMAL(18, 4)
AS
BEGIN
    DECLARE @result DECIMAL(18, 4)

    SET @result = @initialSum * (POWER((1 + @yearlyInterestRate), @numberOfYears))

    RETURN @result
END
GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)