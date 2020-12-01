  SELECT c.CountryName AS [Country Name],
         c.IsoCode AS [ISO Code]
    FROM Countries AS c
   WHERE c.CountryName LIKE '%a%a%a%'
ORDER BY c.IsoCode