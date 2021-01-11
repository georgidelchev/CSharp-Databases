SELECT TOP (5) c.CountryName,
               MAX(p.Elevation) AS HighestPeakElevation,
               MAX(r.Length)    AS LongestRiverLength
    FROM Countries AS c
             LEFT OUTER JOIN CountriesRivers AS cr
                             ON c.CountryCode = cr.CountryCode
             LEFT OUTER JOIN Rivers AS r
                             ON cr.RiverId = r.Id
             LEFT OUTER JOIN MountainsCountries AS mc
                             ON c.CountryCode = mc.CountryCode
             LEFT OUTER JOIN Mountains AS m
                             ON mc.MountainId = m.Id
             LEFT OUTER JOIN Peaks p
                             ON m.Id = p.MountainId
    GROUP BY c.CountryName
    ORDER BY HighestPeakElevation DESC,
             LongestRiverLength DESC,
             c.CountryName
