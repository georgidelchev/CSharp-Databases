    SELECT ct.ContinentName,
           SUM(c.AreaInSqKm) AS [CountriesArea],
           SUM(CAST(c.Population AS BIGINT)) AS [CountriesPopulation]
      FROM Continents ct
INNER JOIN Countries c
        ON c.ContinentCode = ct.ContinentCode
  GROUP BY ct.ContinentName
  ORDER BY [CountriesPopulation] DESC