CREATE TABLE Users (
    Id INT IDENTITY(1, 1),
    Username VARCHAR(30) NOT NULL,
    Password VARCHAR(26) NOT NULL,
    ProfilePicture VARBINARY(MAX),
    LastLoginTime SMALLDATETIME,
    IsDeleted BIT

    CONSTRAINT PK_Users
    PRIMARY KEY (Id),

    CONSTRAINT UQ_Username
    UNIQUE (Username)
)

INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
     VALUES ('goshko', 'abcdefghijklmnopqrstuvwxyz', 123456, '2017-11-09 11:37:15', 1),
            ('peshko', '12345', 55555, '2017-11-09 11:37:15', 1),
            ('ivan84', '12aaaa1234', NULL, '2017-10-22 12:55:17', 0),
            ('mara', '**ad**ad**ad', NULL, '2010-05-30 05:02:00', 0),
            ('dim4o', 'dim44444444', 1, '2016-02-14 23:01:55', 1)