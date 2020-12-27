CREATE TABLE City
(
    Id          INT PRIMARY KEY IDENTITY,
    Name        NVARCHAR(17) NOT NULL,
    CountryCode NVARCHAR(3)  NOT NULL,
    District    NVARCHAR(20) NOT NULL,
    Population  INT          NOT NULL
)

CREATE TABLE Country
(
    Id             INT PRIMARY KEY IDENTITY,
    Code           NVARCHAR(3)    NOT NULL,
    Name           NVARCHAR(44)   NOT NULL,
    Continent      NVARCHAR(13)   NOT NULL,
    Region         NVARCHAR(25)   NOT NULL,
    SurfaceArea    INT            NOT NULL,
    IndepYear      DATE           NOT NULL,
    Population     INT            NOT NULL,
    LifeExpectancy NVARCHAR(4)    NOT NULL,
    GNP            DECIMAL(18, 4) NOT NULL,
    GNPOLD         DECIMAL(18, 4) NOT NULL,
    LocalName      NVARCHAR(44)   NOT NULL,
    GovernmentForm NVARCHAR(44)   NOT NULL,
    HeadOfState    NVARCHAR(32)   NOT NULL,
    Capital        NVARCHAR(20)   NOT NULL,
    Code2          NVARCHAR(10)   NOT NULL
)

--1
SELECT SUM(c.Population)
    FROM City AS c
    WHERE c.CountryCode IN (SELECT ct.Code
                                FROM Country AS ct
                                WHERE ct.Continent = 'Asia')

--2
SELECT SUM(c.Population)
    FROM City AS c
             JOIN Country AS c2 ON c2.Code = c.CountryCode
    WHERE c2.Continent = 'Asia'

--3
SELECT SUM(c.Population)
    FROM City AS c,
         Country AS ct
    WHERE c.CountryCode = ct.Code
      AND ct.Continent = 'Asia'