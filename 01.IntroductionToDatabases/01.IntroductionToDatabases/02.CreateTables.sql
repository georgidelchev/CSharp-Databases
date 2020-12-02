CREATE TABLE Clients (
    ClientId INT IDENTITY(1, 1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_ClientId
    PRIMARY KEY (ClientId)
)

CREATE TABLE AccountTypes (
    AccountTypeId INT IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_AccountTypeId
    PRIMARY KEY (AccountTypeId)
)

CREATE TABLE Accounts (
    AccountId INT IDENTITY(1, 1),
    AccountTypeId INT,
    ClientId INT,
    Balance DECIMAL(15, 2) NOT NULL DEFAULT(0),

    CONSTRAINT PK_AccountId
    PRIMARY KEY (AccountId),

    CONSTRAINT FK_Accounts_AccountTypes
    FOREIGN KEY (AccountTypeId)
    REFERENCES AccountTypes(AccountTypeId),

    CONSTRAINT PK_Accounts_Clients
    FOREIGN KEY (ClientId)
    REFERENCES Clients(ClientId)
)