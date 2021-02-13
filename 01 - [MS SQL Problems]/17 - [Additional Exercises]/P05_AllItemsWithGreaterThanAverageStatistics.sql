SELECT i.Name,
       i.Price,
       i.MinLevel,
       S.Strength,
       S.Defence,
       S.Speed,
       S.Luck,
       S.Mind
    FROM Items AS i
             JOIN [Statistics] S
                  ON S.Id = i.StatisticId
    WHERE S.Mind > (SELECT AVG(s.Mind)
                        FROM [Statistics] AS s)
      AND S.Luck > (SELECT AVG(s.Luck)
                        FROM [Statistics] AS s)
      AND S.Speed > (SELECT AVG(s.Speed)
                         FROM [Statistics] AS s)
    ORDER BY i.Name