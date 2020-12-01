CREATE FUNCTION ufn_CalculateFutureValue (@Sum MONEY, @YIR FLOAT, @NumberOfYears INT)
RETURNS MONEY
AS
BEGIN
    DECLARE @FutureValue MONEY
    SET @FutureValue = @Sum * (POWER(1 + @YIR, @NumberOfYears))
    
    RETURN @FutureValue
END