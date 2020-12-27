INSERT INTO City(name, CountryCode, District, Population)
    VALUES ('California', 'CF', 'California', 5000000)

SELECT SUM(c.Population)
    FROM City AS c
    WHERE c.District = 'California'