DECLARE @userId INT= (SELECT Id
                          FROM Users
                          WHERE Username = 'Alex');

DECLARE @gameId INT= (SELECT Id
                          FROM Games
                          WHERE Name = 'Edinburgh');

DECLARE @ugId INT= (SELECT Id
                        FROM UsersGames
                        WHERE UserId = @userId
                          AND GameId = @gameId);

UPDATE UsersGames
SET Cash -= (SELECT SUM(I.Price)
                 FROM Items AS I
                 WHERE I.Name IN ('Blackguard', 'Bottomless Potion of Amplification',
                                  'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin',
                                  'Golden Gorget of Leoric', 'Hellfire Amulet'))
    WHERE Id = @ugId;

INSERT INTO UserGameItems(ItemId, UserGameId)
    VALUES ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Blackguard'), @ugId),
           ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Bottomless Potion of Amplification'), @ugId),
           ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Eye of Etlich (Diablo III)'), @ugId),
           ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Gem of Efficacious Toxin'), @ugId),
           ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Golden Gorget of Leoric'), @ugId),
           ((SELECT i.Id
                 FROM Items AS i
                 WHERE Name = 'Hellfire Amulet'), @ugId)

SELECT U.Username,
       G.Name,
       UG.Cash,
       I.Name
    FROM Users AS U
             JOIN UsersGames UG
                  ON U.Id = UG.UserId
             JOIN Games G
                  ON G.Id = UG.GameId
             JOIN UserGameItems UGI
                  ON UG.Id = UGI.UserGameId
             JOIN Items I
                  ON I.Id = UGI.ItemId
    WHERE G.Name = 'Edinburgh'
    ORDER BY I.Name