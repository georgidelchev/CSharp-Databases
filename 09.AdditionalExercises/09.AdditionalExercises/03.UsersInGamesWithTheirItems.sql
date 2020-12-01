    SELECT u.Username AS [Username],
           g.Name AS [Game],
           COUNT(i.Id) AS [Items Count],
           SUM(i.Price) AS [Items Price]
      FROM Games g
INNER JOIN UsersGames ug
        ON ug.GameId = g.Id
INNER JOIN Users u
        ON u.Id = ug.UserId
INNER JOIN UserGameItems ugi
        ON ugi.UserGameId = ug.Id
INNER JOIN Items i
        ON i.Id = ugi.ItemId
  GROUP BY u.Username, 
           g.Name
    HAVING COUNT(i.Id) >= 10
  ORDER BY [Items Count] DESC,
           [Items Price] DESC,
           u.Username