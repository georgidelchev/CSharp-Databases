CREATE PROC p_AddAccount @ClientId INT, @AccountTypeId INT 
AS
INSERT INTO Accounts (ClientId, AccountTypeId)
     VALUES (@ClientId, @AccountTypeId)
GO

CREATE PROC p_Deposit @AccountId INT, @Amount DECIMAL(15, 2)
AS
UPDATE Accounts
   SET Balance += @Amount
 WHERE AccountId = @AccountId
GO

CREATE PROC p_Withdraw @AccountId INT, @Amount DECIMAL(15, 2)
AS
BEGIN
    DECLARE @OldBalance DECIMAL(15, 2)

    SELECT @OldBalance = Balance
      FROM Accounts
     WHERE ClientId = @AccountId
    IF (@OldBalance - @Amount >= 0)
    BEGIN
        UPDATE Accounts
           SET Balance -= @Amount
         WHERE AccountId = @AccountId
    END
    ELSE
    BEGIN
        RAISERROR('Insufficient funds', 10, 1)
    END
END
GO