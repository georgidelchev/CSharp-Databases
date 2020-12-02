    SELECT t.Name AS [TownName],
           COUNT(o.Id) AS [OfficesNumber]
      FROM Towns t
INNER JOIN Offices o
        ON o.TownId = t.Id
  GROUP BY t.Name
  ORDER BY [OfficesNumber] DESC,
           [TownName]