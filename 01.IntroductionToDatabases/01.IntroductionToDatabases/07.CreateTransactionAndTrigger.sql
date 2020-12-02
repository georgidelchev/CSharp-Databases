CREATE TABLE Transactions (
    Id INT IDENTITY(1, 1),
    AccountId INT,
    OldBalance DECIMAL(15, 2) NOT NULL,
    NewBalance DECIMAL(15, 2) NOT NULL,
    Amount AS NewBalance - OldBalance,
    DateTime DATETIME2,

    CONSTRAINT PK_Id
    PRIMARY KEY (Id),

    CONSTRAINT FK_Transactions_Accounts
    FOREIGN KEY (AccountId)
    REFERENCES Accounts(AccountId)
)
GO

CREATE TRIGGER tr_Transaction ON Accounts
AFTER UPDATE
AS
    INSERT INTO Transactions (AccountId, OldBalance, NewBalance, DateTime)
         SELECT inserted.ClientId, 
                deleted.Balance, 
                inserted.Balance, 
                GETDATE()
           FROM inserted
           JOIN deleted 
             ON inserted.AccountId = deleted.AccountId
GO

EXEC p_Deposit 1, 25
EXEC p_Deposit 1, 40.00
EXEC p_Withdraw 2, 200.00
EXEC p_Deposit 4, 180.00