CREATE FUNCTION ufn_CashInUsersGames (@GameName NVARCHAR(50))
RETURNS TABLE
AS
RETURN (
    WITH CTE_CashInRows (Cash, RowNumber)
    AS
    (
         SELECT ug.Cash,
                ROW_NUMBER() OVER (ORDER BY ug.Cash DESC)
           FROM UsersGames ug
     INNER JOIN Games g
             ON g.Id = ug.GameId
          WHERE g.Name = @GameName
    )

    SELECT SUM(Cash) AS [SumCash]
      FROM CTE_CashInRows
     WHERE RowNumber % 2 = 1
)