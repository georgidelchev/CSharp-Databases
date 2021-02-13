SELECT c.CountryName,
       C2.ContinentName,
       ISNULL(COUNT(R2.Id), 0)   AS Count,
       ISNULL(SUM(R2.Length), 0) AS Sum
    FROM Countries AS c
             LEFT JOIN CountriesRivers Cr ON c.CountryCode = Cr.CountryCode
             LEFT JOIN Rivers R2 ON R2.Id = Cr.RiverId
             JOIN Continents C2 ON C2.ContinentCode = c.ContinentCode
    GROUP BY c.CountryName,
             C2.ContinentName
    ORDER BY Count DESC,
             Sum DESC,
             c.CountryName