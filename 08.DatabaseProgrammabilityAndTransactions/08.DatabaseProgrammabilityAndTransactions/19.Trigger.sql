CREATE TRIGGER tr_UserGameItems 
ON UserGameItems 
INSTEAD OF INSERT 
AS
BEGIN 
    INSERT INTO UserGameItems
         SELECT i.Id, 
                ug.Id 
           FROM inserted
      INNER JOIN UsersGames AS ug
             ON UserGameId = ug.Id
     inner JOIN Items AS i
             ON ItemId = i.Id
          WHERE ug.Level >= i.MinLevel
END
GO

    UPDATE UsersGames
       SET Cash += 50000
      FROM UsersGames AS ug
INNER JOIN Users AS u
        ON ug.UserId = u.Id
INNER JOIN Games AS g
        ON ug.GameId = g.Id
     WHERE g.Name = 'Bali' 
       AND u.Username IN ('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
GO

CREATE PROC usp_BuyItems(@Username VARCHAR(100)) 
AS
BEGIN
    DECLARE @UserId INT = (SELECT Id FROM Users WHERE Username = @Username)
    DECLARE @GameId INT = (SELECT Id FROM Games WHERE Name = 'Bali')
    DECLARE @UserGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
    DECLARE @UserGameLevel INT = (SELECT Level FROM UsersGames WHERE Id = @UserGameId)

    DECLARE @counter INT = 251

    WHILE(@counter <= 539)
    BEGIN
        DECLARE @ItemId INT = @counter
        DECLARE @ItemPrice MONEY = (SELECT Price FROM Items WHERE Id = @ItemId)
        DECLARE @ItemLevel INT = (SELECT MinLevel FROM Items WHERE Id = @ItemId)
        DECLARE @UserGameCash MONEY = (SELECT Cash FROM UsersGames WHERE Id = @UserGameId)

        IF(@UserGameCash >= @ItemPrice AND @UserGameLevel >= @ItemLevel)
        BEGIN
            UPDATE UsersGames
            SET Cash -= @ItemPrice
            WHERE Id = @UserGameId

            INSERT INTO UserGameItems VALUES
            (@ItemId, @UserGameId)
        END

        SET @counter += 1
        
        IF(@counter = 300)
        BEGIN
            SET @counter = 501
        END
    END
END