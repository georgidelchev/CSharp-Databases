CREATE OR
ALTER FUNCTION ufn_CashInUsersGames(@gameName NVARCHAR(MAX))
    RETURNS TABLE
        AS
        RETURN SELECT SUM(Cash) AS SumCash
                   FROM (SELECT g.Name,
                                Ug.Cash,
                                ROW_NUMBER() OVER ( PARTITION BY g.Name ORDER BY Ug.Cash DESC ) AS RowNum
                             FROM Games AS g
                                      JOIN UsersGames Ug ON g.Id = Ug.GameId
                             WHERE g.Name = @gameName) AS Row
                   WHERE RowNum % 2 != 0


SELECT *
    FROM dbo.ufn_CashInUsersGames('Love in a mist')