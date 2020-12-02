  SELECT TOP 30
         c.CountryName,
         c.Population
    FROM Countries AS c
   WHERE c.ContinentCode = 'EU'
ORDER BY c.Population DESC