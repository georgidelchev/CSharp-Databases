CREATE TABLE NotificationEmails (
    Id INT NOT NULL IDENTITY(1, 1),
    Recipient INT NOT NULL,
    Subject NVARCHAR(50) NOT NULL,
    Body NVARCHAR(100) NOT NULL,

    CONSTRAINT PK_NotificationEmails PRIMARY KEY (Id),
    CONSTRAINT FK_NotificationEmails_Accounts FOREIGN KEY (Recipient) REFERENCES Accounts (Id)
)
GO

CREATE TRIGGER tr_EmailNotification
ON Logs
AFTER INSERT
AS 
BEGIN
    INSERT INTO NotificationEmails (Recipient, Subject, Body)
         SELECT i.AccountId,
                CONCAT('Balance change for account: ', i.AccountId),
                CONCAT('On ', GETDATE(), ' your balance was changed from ', i.OldSum, ' to ', i.NewSum, '.')
           FROM INSERTED i
END