DECLARE @UserId INT = (SELECT Id FROM Users WHERE Username = 'Alex')
DECLARE @GameId INT = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
DECLARE @AlexUserGameId INT = (SELECT Id FROM UsersGames WHERE GameId = @GameId AND UserId = @UserId)
DECLARE @TotalPrice MONEY = (SELECT SUM(Price) FROM Items WHERE Name IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet'))
DECLARE @AlexGameId INT = (SELECT GameId FROM UsersGames WHERE Id = @AlexUserGameId)

INSERT INTO UserGameItems
     SELECT i.Id, @AlexUserGameId
       FROM Items AS i
      WHERE i.Name IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet')

UPDATE UsersGames
   SET Cash -= @TotalPrice
 WHERE Id = @AlexUserGameId

    SELECT u.Username [Username],
           g.Name AS [Name],
           ug.Cash [Cash],
           i.Name AS [Item name]
      FROM Users AS u
INNER JOIN UsersGames AS ug
        ON ug.UserId = u.Id
INNER JOIN Games AS g
        ON g.Id = ug.GameId
INNER JOIN UserGameItems AS ugi
        ON ugi.UserGameId = ug.Id
INNER JOIN Items AS i
        ON i.Id = ugi.ItemId
     WHERE ug.GameId = @AlexGameId
  ORDER BY [Item name]