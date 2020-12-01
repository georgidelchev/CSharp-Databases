  SELECT c.CountryName,
         c.CountryCode,
         CASE 
             WHEN c.CurrencyCode = 'EUR' THEN 'Euro'
             ELSE 'Not Euro'
         END AS [Currency]
    FROM Countries AS c
ORDER BY c.CountryName