    SELECT p.PeakName,
           m.MountainRange AS [Mountain],
           c.CountryName,
           ct.ContinentName
      FROM Peaks p
INNER JOIN Mountains m
        ON m.Id = p.MountainId
INNER JOIN MountainsCountries mc
        ON mc.MountainId = m.Id
INNER JOIN Countries c
        ON c.CountryCode = mc.CountryCode
INNER JOIN Continents ct
        ON ct.ContinentCode = c.ContinentCode
  ORDER BY p.PeakName,
           c.CountryName