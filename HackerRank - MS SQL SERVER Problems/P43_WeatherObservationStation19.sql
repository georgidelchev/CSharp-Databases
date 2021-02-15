-- (X)^2 + (Y)^2 = (Distance)^2

SELECT CAST(SQRT(POWER(MAX(LAT_N) - MIN(LAT_N), 2)
    + POWER(MAX(LONG_W) - MIN(LONG_W), 2)) AS DECIMAL(18, 4))
    FROM Station