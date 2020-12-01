    SELECT g.Name AS [Game],
           gt.Name AS [Game Type],
           u.Username AS [Username],
           ug.Level AS [Level],
           ug.Cash AS [Cash],
           c.Name AS [Character]
      FROM Games AS g
INNER JOIN GameTypes gt
        ON gt.Id = g.GameTypeId
INNER JOIN UsersGames ug
        ON ug.GameId = g.Id
INNER JOIN Users u
        ON u.Id = ug.UserId
INNER JOIN Characters c
        ON c.Id = ug.CharacterId
  ORDER BY [Level] DESC,
           [Username],
           [Game]