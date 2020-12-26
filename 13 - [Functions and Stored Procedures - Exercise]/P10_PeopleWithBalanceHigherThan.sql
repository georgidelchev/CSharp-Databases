CREATE OR
ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan(@balance decimal(18, 4))
AS
SELECT ah.FirstName, ah.LastName
    FROM AccountHolders AS ah
             JOIN Accounts A ON ah.Id = A.AccountHolderId
    GROUP BY ah.FirstName, ah.LastName
    HAVING SUM(A.Balance) > @balance
    ORDER BY ah.FirstName, ah.LastName
GO

EXEC usp_GetHoldersWithBalanceHigherThan 15000