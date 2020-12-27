SELECT ct.Continent, FLOOR(AVG(c.Population))
    FROM City AS c
             JOIN Country AS ct ON ct.Code = c.CountryCode
    GROUP BY ct.Continent