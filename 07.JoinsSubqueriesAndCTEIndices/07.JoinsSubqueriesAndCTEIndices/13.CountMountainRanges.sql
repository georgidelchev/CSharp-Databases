    SELECT c.CountryCode,
           COUNT(m.Id) AS [MountainRanges]
      FROM Mountains m
INNER JOIN MountainsCountries mc
        ON mc.MountainId = m.Id
INNER JOIN Countries c
        ON c.CountryCode = mc.CountryCode
     WHERE c.CountryCode IN ('US', 'RU', 'BG')
  GROUP BY c.CountryCode