    SELECT u.Username AS [Username],
           g.Name AS [Game],
           MAX(c.Name) AS [Character],
           SUM(ist.Strength) + MAX(gst.Strength) + MAX(cs.Strength) AS [Strength],
           SUM(ist.Defence) + MAX(gst.Defence) + MAX(cs.Defence) AS [Defence],
           SUM(ist.Speed) + MAX(gst.Speed) + MAX(cs.Speed) AS [Speed],
           SUM(ist.Mind) + MAX(gst.Mind) + MAX(cs.Mind) AS [Mind],
           SUM(ist.Luck) + MAX(gst.Luck) + MAX(cs.Luck) AS [Luck]
      FROM Users AS u
INNER JOIN UsersGames AS ug
        ON ug.UserId = u.Id
INNER JOIN Games AS g
        ON g.Id = ug.GameId
INNER JOIN Characters AS c
        ON c.Id = ug.CharacterId
INNER JOIN GameTypes AS gt
        ON gt.Id = g.GameTypeId
INNER JOIN [Statistics] AS gst
        ON gst.Id = gt.BonusStatsId
INNER JOIN [Statistics] AS cs
        ON cs.Id = c.StatisticId
INNER JOIN UserGameItems AS ugi
        ON ugi.UserGameId = ug.Id
INNER JOIN Items AS i
        ON i.Id = ugi.ItemId
INNER JOIN [Statistics] AS ist
        ON ist.Id = i.StatisticId
  GROUP BY u.Username, 
           g.Name
  ORDER BY [Strength] DESC, 
           [Defence] DESC, 
           [Speed] DESC, 
           [Mind] DESC, 
           [Luck] DESC