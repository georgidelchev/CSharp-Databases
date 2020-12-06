INSERT INTO Station(CITY, STATE, LAT_N, LONG_W)
    VALUES ('New York', 'US', 20, 20),
           ('New York', 'US', 20, 20),
           ('Bengalaru', 'SW', 20, 20)

SELECT COUNT(CITY) - COUNT(DISTINCT CITY) AS DiffBetweenTotalCitiesAndDistinctCities
    FROM Station
