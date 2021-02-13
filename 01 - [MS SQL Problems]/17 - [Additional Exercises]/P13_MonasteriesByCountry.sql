CREATE TABLE Monasteries
(
    Id          INT PRIMARY KEY IDENTITY,
    Name        NVARCHAR(100) NOT NULL,
    CountryCode CHAR(2)       NOT NULL
)

INSERT INTO Monasteries(Name, CountryCode)
    VALUES ('Rila Monastery “St. Ivan of Rila”', 'BG'),
           ('Bachkovo Monastery “Virgin Mary”', 'BG'),
           ('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
           ('Kopan Monastery', 'NP'),
           ('Thrangu Tashi Yangtse Monastery', 'NP'),
           ('Shechen Tennyi Dargyeling Monastery', 'NP'),
           ('Benchen Monastery', 'NP'),
           ('Southern Shaolin Monastery', 'CN'),
           ('Dabei Monastery', 'CN'),
           ('Wa Sau Toi', 'CN'),
           ('Lhunshigyia Monastery', 'CN'),
           ('Rakya Monastery', 'CN'),
           ('Monasteries of Meteora', 'GR'),
           ('The Holy Monastery of Stavronikita', 'GR'),
           ('Taung Kalat Monastery', 'MM'),
           ('Pa-Auk Forest Monastery', 'MM'),
           ('Taktsang Palphug Monastery', 'BT'),
           ('S?mela Monastery', 'TR')

ALTER TABLE Countries
    ADD IsDeleted BIT DEFAULT 0

UPDATE Countries
SET IsDeleted=1
    WHERE Countrycode IN
          (SELECT Q1.CountryCode
               FROM (SELECT C.Countrycode,
                            COUNT(CR.Riverid) AS COUNT
                         FROM Countries AS C
                                  JOIN Countriesrivers CR
                                       ON C.Countrycode = CR.Countrycode
                         GROUP BY C.Countrycode) AS Q1
               WHERE Q1.COUNT > 3)

SELECT M.Name        AS Monastery,
       C.Countryname AS Country
    FROM Monasteries AS M
             JOIN Countries C
                  ON M.CountryCode = C.Countrycode
    WHERE C.IsDeleted = 0
    ORDER BY Monastery