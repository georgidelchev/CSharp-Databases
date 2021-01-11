CREATE TABLE Users
(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX)
	CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024),
	LastLoginTime DATETIME2,
	IsDeleted BIT
)

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
	VALUES('ghostHunter201', '6345765', 1200, '2016-10-10 13:41:42', 0),
	      ('shadow5665', '634655', 1500, '2020-05-10 01:53:43', 0),
	      ('easyPeasy', '12333', 1120, '2019-10-10 23:42:43', 0),
	      ('CakePie', '152552', 1333, '2018-11-07 15:51:43', 0),
	      ('CatOnAJet', '262363', 1764, '2017-10-11 11:15:43', 1);

UPDATE Users
	SET [Password] = '1234567'
WHERE Id = 2;

-- SHALL THROW AN EXCEPTION --
INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
	VALUES('TEST', '111', 1, '2016-10-10 13:41:42', 0)
-- SHALL THROW AN EXCEPTION --

-- SHALL SET DEFAULT CURRENT LOGIN TIME --
INSERT INTO Users(Username, [Password], ProfilePicture, IsDeleted)
	VALUES('TEST123', '1111111', 1 , 0)
-- SHALL SET DEFAULT CURRENT LOGIN TIME --

-- SHALL THROW AN EXCEPTION --
INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
	VALUES('TE', '111111', 1, '2016-10-10 13:41:42', 0)
-- SHALL THROW AN EXCEPTION --

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
	VALUES('Simona', '123456', 11231, '2016-10-10 13:41:42', 0)

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
	VALUES('Aleksandra', 'veryHardcorePassword123321', 11211131, '2016-10-10 13:41:42', 0)


SELECT * 
	FROM Users
