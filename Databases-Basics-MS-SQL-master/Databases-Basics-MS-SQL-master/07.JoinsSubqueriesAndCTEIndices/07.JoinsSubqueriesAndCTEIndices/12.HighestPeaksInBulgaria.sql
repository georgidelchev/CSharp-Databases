    SELECT c.CountryCode,
           m.MountainRange,
           p.PeakName,
           p.Elevation
      FROM Peaks p
INNER JOIN Mountains m
        ON m.Id = P.MountainId
INNER JOIN MountainsCountries mc
        ON mc.MountainId = m.Id
INNER JOIN Countries c
        ON c.CountryCode = mc.CountryCode
     WHERE c.CountryCode = 'BG'
       AND p.Elevation > 2835
  ORDER BY p.Elevation DESC