INSERT INTO Station(CITY, STATE, LAT_N, LONG_W)
    VALUES ('ABC', 'SW', 20, 20),
           ('DEF', 'SW', 20, 20),
           ('PQRS', 'SW', 20, 20),
           ('WXY', 'SW', 20, 20)

SELECT TOP (1) CITY, LEN(CITY) AS CityLen
    FROM Station
    ORDER BY CityLen, CITY

SELECT TOP (1) CITY, LEN(CITY) AS CityLen
    FROM Station
    ORDER BY CityLen DESC, CITY

SELECT *
    FROM Station
    ORDER BY CITY
