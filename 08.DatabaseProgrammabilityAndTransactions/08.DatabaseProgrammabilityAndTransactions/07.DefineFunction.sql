CREATE FUNCTION ufn_isWordComprised (@SetOfLetters NVARCHAR(50), @Word NVARCHAR(50))
RETURNS BIT
AS
BEGIN
    DECLARE @Index INT = 1
    WHILE (@Index <= LEN(@Word))
    BEGIN
        DECLARE @Symbol NVARCHAR(1) = SUBSTRING(@Word, @Index, 1)
        IF (CHARINDEX(@Symbol, @SetOfLetters, 1) = 0)
        BEGIN
            RETURN 0
        END
        SET @Index += 1
    END
    RETURN 1
END