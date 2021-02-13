SELECT p.PeakName,
       M.MountainRange,
       C.CountryName,
       C2.ContinentName
    FROM Peaks AS p
             JOIN MountainsCountries Mc
                  ON p.MountainId = Mc.MountainId
             JOIN Mountains M
                  ON M.Id = Mc.MountainId
             JOIN Countries C
                  ON C.CountryCode = Mc.CountryCode
             JOIN Continents C2
                  ON C2.ContinentCode = C.ContinentCode
    ORDER BY p.PeakName,
             C.CountryName