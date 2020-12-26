CREATE OR
ALTER FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX))
    RETURNS BIT
AS
BEGIN
    DECLARE @index INT = 1
    DECLARE @wordLength INT = LEN(@word)

    WHILE(@index <= @wordLength)
        BEGIN
            DECLARE @symbol NVARCHAR(1) = SUBSTRING(@word, @index, 1)

            IF (CHARINDEX(@symbol, @setOfLetters, 1) = 0)
                RETURN 0

            SET @index += 1
        END

    RETURN 1
END
GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia'),
       dbo.ufn_IsWordComprised('oistmiahf', 'halves'),
       dbo.ufn_IsWordComprised('bobr', 'Rob'),
       dbo.ufn_IsWordComprised('pppp', 'Guy')
