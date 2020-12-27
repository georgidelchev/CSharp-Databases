INSERT INTO City(name, CountryCode, District, Population)
    VALUES ('California', 'CF', 'California', 6000000)

SELECT AVG(c.Population)
    FROM City AS c
    WHERE c.District = 'California'