WITH cte_AllMalePeople
AS
(
        SELECT t.Id AS [TownId],
               t.Name AS [Town],
               COUNT(c.Id) AS [MaleCount]
          FROM Clients c
     LEFT JOIN Orders o
            ON o.ClientId = c.Id
     LEFT JOIN Towns t
            ON t.Id = o.TownId
         WHERE c.Gender = 'M'
      GROUP BY t.Name,
               t.Id
),
cte_AllFemalePeople
AS
(
        SELECT t.Id AS [TownId],
               t.Name AS [Town],
               COUNT(c.Id) AS [FemaleCount]
          FROM Clients c
     LEFT JOIN Orders o
            ON o.ClientId = c.Id
     LEFT JOIN Towns t
            ON t.Id = o.TownId
         WHERE c.Gender = 'F'
      GROUP BY t.Name,
               t.Id
)

   SELECT t.Name AS [TownName],
          CAST(m.MaleCount * 100 / (ISNULL(m.MaleCount, 0) + ISNULL(f.FemaleCount, 0)) AS INT) AS [MalePercent],
          CAST(f.FemaleCount * 100 / (ISNULL(m.MaleCount, 0) + ISNULL(f.FemaleCount, 0)) AS INT) AS [FemalePercent]
     FROM Towns t
LEFT JOIN cte_AllMalePeople m
       ON m.TownId = t.Id
LEFT JOIN cte_AllFemalePeople f
       ON f.TownId = t.Id
 ORDER BY t.Name,
          t.Id
