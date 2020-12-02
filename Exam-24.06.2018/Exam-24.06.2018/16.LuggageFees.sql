  SELECT ats.TripId,
         SUM(ats.Luggage) AS [Luggage],
         CASE 
             WHEN SUM(ats.Luggage) > 5 THEN '$' + CAST(SUM(ats.Luggage) * 5 AS VARCHAR)
             ELSE '$0'
         END
    FROM AccountsTrips ats
   WHERE ats.Luggage >= 1
GROUP BY ats.TripId
ORDER BY [Luggage] DESC