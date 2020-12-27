SELECT c.Name
    FROM City AS c
             JOIN Country AS ct ON ct.Code = c.CountryCode
    WHERE ct.Continent = 'Africa'