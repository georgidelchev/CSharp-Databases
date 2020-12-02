CREATE PROC usp_TransferMoney (@SenderId INT, @ReceiverId INT, @Amount DECIMAL(18, 4))
AS
BEGIN
    BEGIN TRANSACTION
        IF (@Amount <= 0)
        BEGIN
            RAISERROR('Amount cannot be negative or zero', 16, 1)
            ROLLBACK
            RETURN
        END
        EXEC usp_DepositMoney @ReceiverId, @Amount
        EXEC usp_WithdrawMoney @SenderId, @Amount
        COMMIT
END