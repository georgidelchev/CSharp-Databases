WITH cte_AccountsWithTrips
AS
(
        SELECT a.Id,
               a.FirstName + ' ' + a.LastName AS [FullName],
               DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate) AS [Days]
          FROM Accounts a
    INNER JOIN AccountsTrips ats
            ON ats.AccountId = a.Id
    INNER JOIN Trips t
            ON T.Id = ats.TripId
         WHERE T.CancelDate IS NULL 
           AND a.MiddleName IS NULL
)

  SELECT cte.Id,
         cte.FullName,
         MAX(cte.[Days]) AS [LongestTrip],
         MIN(cte.[Days]) AS [ShortestTrip]
    FROM cte_AccountsWithTrips cte
GROUP BY cte.FullName, cte.Id
ORDER BY [LongestTrip] DESC,
         cte.Id