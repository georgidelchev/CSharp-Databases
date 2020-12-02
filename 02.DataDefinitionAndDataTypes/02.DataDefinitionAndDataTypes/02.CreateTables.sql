CREATE TABLE Minions (
    Id INT,
    Name NVARCHAR(50) NOT NULL,
    Age INT,

    CONSTRAINT PK_Minions
    PRIMARY KEY (ID)
)

CREATE TABLE Towns (
    Id INT,
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Towns
    PRIMARY KEY (Id)
)