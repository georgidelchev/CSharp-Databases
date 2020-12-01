SELECT TOP 50
           g.Name,
           FORMAT(g.Start, 'yyyy-MM-dd') AS [Start]
      FROM Games AS g
     WHERE YEAR(g.Start) IN (2011, 2012)
  ORDER BY [Start],
           g.Name