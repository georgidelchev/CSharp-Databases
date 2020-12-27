DECLARE
    @counter NVARCHAR(MAX) = 20

WHILE (@counter > 0)
    BEGIN
        SELECT REPLICATE('* ', @counter)

        SET @counter -= 1
    END
