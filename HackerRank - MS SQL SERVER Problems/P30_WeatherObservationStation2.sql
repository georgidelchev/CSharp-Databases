CREATE TABLE Station
(
    ID     INT PRIMARY KEY IDENTITY,
    CITY   NVARCHAR(20) NOT NULL,
    STATE  NVARCHAR(20) NOT NULL,
    LAT_N  INT          NOT NULL,
    LONG_W INT          NOT NULL
)

INSERT INTO Station (CITY, STATE, LAT_N, LONG_W)
    VALUES ('Sofia1', 'SF', 10, 10),
           ('Sofia2', 'SF', 20, 20),
           ('Sofia3', 'SF', 30, 30),
           ('Sofia4', 'SF', 40, 40),
           ('Sofia5', 'SF', 50, 50)

SELECT FORMAT(ROUND(SUM(LAT_N), 2), 'F2'),
       FORMAT(ROUND(SUM(LONG_W), 2), 'F2')
    FROM Station
