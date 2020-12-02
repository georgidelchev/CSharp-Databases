CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN
    BEGIN TRANSACTION
        UPDATE Accounts
           SET Balance += @MoneyAmount
         WHERE Id = @AccountId
    COMMIT
END