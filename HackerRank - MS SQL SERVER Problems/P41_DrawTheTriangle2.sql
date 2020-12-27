DECLARE
    @counter NVARCHAR(MAX) = 1

WHILE (@counter <= 20)
    BEGIN
        SELECT REPLICATE('* ', @counter)

        SET @counter += 1
    END
