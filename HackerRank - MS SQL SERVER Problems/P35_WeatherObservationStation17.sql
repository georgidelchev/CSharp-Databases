-- 1
SELECT FORMAT(ROUND(LONG_W, 4), 'F4')
    FROM Station
    WHERE LAT_N = (SELECT MIN(LAT_N)
                       FROM Station
                       WHERE LAT_N > 38.7780)

-- 2
SELECT CAST(LONG_W AS DECIMAL(20, 4))
    FROM Station
    WHERE LAT_N = (SELECT MIN(LAT_N)
                       FROM Station
                       WHERE LAT_N > 38.7780);