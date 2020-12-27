CREATE TABLE City
(
    Id          INT PRIMARY KEY IDENTITY,
    name        NVARCHAR(17) NOT NULL,
    CountryCode NVARCHAR(3)  NOT NULL,
    District    NVARCHAR(20) NOT NULL,
    Population  INT          NOT NULL
)

INSERT INTO City(name, CountryCode, District, Population)
    VALUES ('Sofia', 'SF', 'SF', 1500000)

SELECT COUNT(*)
    FROM City AS c
    WHERE c.Population > 100000


