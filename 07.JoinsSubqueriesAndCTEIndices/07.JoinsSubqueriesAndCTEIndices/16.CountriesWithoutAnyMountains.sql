   SELECT COUNT(*) - COUNT(mc.MountainId) AS [CountryCode]
     FROM Countries c
LEFT JOIN MountainsCountries mc
       ON mc.CountryCode = c.CountryCode