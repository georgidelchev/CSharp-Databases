DECLARE
    @number INT = 2

DECLARE
    @result NVARCHAR(1000) = ''

WHILE (@number <= 1000)
    BEGIN
        DECLARE @i INT = @number - 1
        DECLARE @c INT = 1

        WHILE (@i > 1)
            BEGIN
                IF (@number % @i = 0)
                    SET @c = 0

                SET @i -= 1
            END

        IF (@c = 1)
            SET @result += CAST(@number AS NVARCHAR(3)) + '&'

        SET @number += 1
    END

SET @result = SUBSTRING(@result, 1, LEN(@result) - 1)

SELECT @result