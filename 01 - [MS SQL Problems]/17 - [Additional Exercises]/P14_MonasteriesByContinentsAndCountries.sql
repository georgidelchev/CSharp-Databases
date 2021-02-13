UPDATE Countries
SET Countryname='Burma'
    WHERE Countryname = 'Myanmar'

INSERT INTO Monasteries(Name, Countrycode)
    VALUES ('Hanga Abbey', (SELECT Countries.Countrycode
                                FROM Countries
                                WHERE Countryname = 'Tanzania'))

INSERT INTO Monasteries(Name, Countrycode)
    VALUES ('Myin-Tin-Daik', (SELECT Countries.Countrycode
                                  FROM Countries
                                  WHERE Countryname = 'Myanmar'))

SELECT C.Continentname,
       C2.Countryname,
       ISNULL(COUNT(M.Id), 0) AS MonasteriesCount
    FROM Continents AS C
             JOIN Countries C2
                  ON C.Continentcode = C2.Continentcode
                      AND C2.Isdeleted = 0
             LEFT JOIN Monasteries M
                       ON C2.Countrycode = M.Countrycode
    GROUP BY C.Continentname,
             C2.Countryname
    ORDER BY MonasteriesCount DESC,
             C2.Countryname