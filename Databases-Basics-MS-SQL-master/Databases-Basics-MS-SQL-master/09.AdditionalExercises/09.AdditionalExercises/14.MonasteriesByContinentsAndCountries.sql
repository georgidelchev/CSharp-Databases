UPDATE Countries
   SET CountryName = 'Burma'
 WHERE CountryName = 'Myanmar'

INSERT INTO Monasteries (Name, CountryCode)
     VALUES ('Hanga Abbey', (SELECT c.CountryCode FROM Countries c WHERE c.CountryName = 'Tanzania')),
            ('Myin-Tin-Daik', (SELECT c.CountryCode FROM Countries c WHERE c.CountryName = 'Myanmar'))

    SELECT ct.ContinentName,
           c.CountryName,
           COUNT(m.Id) AS [MonasteriesCount] 
      FROM Countries c
INNER JOIN Continents ct
        ON ct.ContinentCode = c.ContinentCode
 LEFT JOIN Monasteries m
        ON m.CountryCode = c.CountryCode
     WHERE c.IsDeleted = 0
  GROUP BY ct.ContinentName,
           c.CountryName
  ORDER BY [MonasteriesCount] DESC,
           c.CountryName