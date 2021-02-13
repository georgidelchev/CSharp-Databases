SELECT TOP (5) Country,
               IIF(PeakName IS NULL, '(no highest peak)', PeakName)       AS HighestPeakName,
               IIF(Elevation IS NULL, 0, Elevation)                       AS HighestPeakElevation,
               IIF(MountainRange IS NULL, '(no mountain)', MountainRange) AS Mountain
    FROM (SELECT *, DENSE_RANK() OVER (PARTITION BY Country ORDER BY Elevation DESC ) AS PeakRank
              FROM (
                       SELECT CountryName AS Country,
                              p.PeakName,
                              p.Elevation,
                              m.MountainRange
                           FROM Countries
                                    LEFT OUTER JOIN MountainsCountries AS mc
                                                    ON Countries.CountryCode = mc.CountryCode
                                    LEFT OUTER JOIN Mountains AS m
                                                    ON mc.MountainId = m.Id
                                    LEFT OUTER JOIN Peaks AS p
                                                    ON m.Id = p.MountainId) AS FullInfoQuery) AS PeakRankings
    WHERE PeakRank = 1
    ORDER BY Country, HighestPeakName

