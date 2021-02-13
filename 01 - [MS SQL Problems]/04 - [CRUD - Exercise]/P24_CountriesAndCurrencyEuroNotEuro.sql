-- 1
SELECT CountryName, CountryCode,
       CASE
           WHEN CurrencyCode = 'EUR' THEN 'Euro'
           ELSE 'Not Euro'
           END AS Currency
    FROM Countries
    ORDER BY CountryName

-- 2
SELECT c.CountryName,
       c.CountryCode,
       IIF(c.CurrencyCode = 'EUR', 'Euro', 'Not Euro')
    FROM Countries AS c
    ORDER BY c.CountryName
