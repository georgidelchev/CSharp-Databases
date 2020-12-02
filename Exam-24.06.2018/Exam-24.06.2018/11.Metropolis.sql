SELECT TOP 5
           c.Id,
           c.Name AS [City],
           c.CountryCode AS [Country],
           COUNT(a.Id) AS [Accounts]
      FROM Cities c
INNER JOIN Accounts a
        ON a.CityId = c.Id
  GROUP BY c.Name,
           c.Id,
           c.CountryCode
  ORDER BY [Accounts] desc