SELECT TOP 1 CAST(PERCENTILE_CONT(0.5)
                                  WITHIN GROUP (ORDER BY LAT_N) OVER () AS DECIMAL(18, 4))
    FROM Station
    ORDER BY LAT_N

INSERT INTO Station(CITY, STATE, LAT_N, LONG_W)
    VALUES ('Sofia', 'SF', 60, 60)

SELECT *
    FROM Station