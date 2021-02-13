SELECT c.CurrencyCode,
       Description,
       COUNT(C2.CountryCode) AS Count
    FROM Currencies AS c
             LEFT JOIN Countries C2
                      ON c.CurrencyCode = C2.CurrencyCode
    WHERE 1 = 1
    HAVING COUNT(*) > 5
    ORDER BY Count DESC,
             Description
