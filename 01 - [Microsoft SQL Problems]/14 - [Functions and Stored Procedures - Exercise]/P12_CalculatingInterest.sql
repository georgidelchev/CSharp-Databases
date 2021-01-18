CREATE PROCEDURE usp_CalculateFutureValueForAccount(@accountId INT, @interestRate FLOAT)
AS
SELECT ah.Id, ah.FirstName, ah.LastName, a.Balance,
       dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5)
    FROM AccountHolders AS ah
             JOIN Accounts AS a ON ah.Id = a.AccountHolderId
    WHERE a.Id = @accountId
GO

EXEC usp_CalculateFutureValueForAccount 1, 0.1
