SELECT FORMAT(ROUND(LONG_W, 4), 'F4')
    FROM Station
    WHERE LAT_N = (SELECT MAX(LAT_N)
                       FROM Station
                       WHERE LAT_N < 137.2345)
