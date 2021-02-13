SELECT c.CountryCode, COUNT(m.MountainRange)
    FROM Countries AS c
             LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
             INNER JOIN Mountains AS m ON mc.MountainId = m.Id
    WHERE c.CountryCode IN ('BG', 'RU', 'US')
    GROUP BY c.CountryCode