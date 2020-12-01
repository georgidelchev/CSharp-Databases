DECLARE @AverageMind DECIMAL = (SELECT AVG(s.Mind) FROM [Statistics] s)
DECLARE @AcerageLuck DECIMAL = (SELECT AVG(s.Luck) FROM [Statistics] s)
DECLARE @AverageSpeed DECIMAL =(SELECT AVG(s.Speed) FROM [Statistics] s)

    SELECT i.Name,
           i.Price,
           i.MinLevel,
           s.Strength,
           s.Defence,
           s.Speed,
           s.Luck,
           s.Mind
FROM Items i
INNER JOIN [Statistics] s
        ON s.Id = i.StatisticId
     WHERE s.Mind > @AverageMind
       AND s.Luck > @AcerageLuck
       AND s.Speed > @AverageSpeed
  ORDER BY i.Name