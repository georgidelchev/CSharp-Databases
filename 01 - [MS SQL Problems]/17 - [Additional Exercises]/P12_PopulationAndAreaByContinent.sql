SELECT c.ContinentName,
       SUM(CAST(C2.AreaInSqKm AS BIGINT)) AS Area,
       SUM(CAST(C2.Population AS BIGINT)) AS Population
    FROM Continents AS c
             JOIN Countries C2
                  ON c.ContinentCode = C2.ContinentCode
    GROUP BY c.ContinentName
    ORDER BY Population DESC