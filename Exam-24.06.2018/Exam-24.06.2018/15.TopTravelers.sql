WITH cte_TopTravelers
AS
(
        SELECT a.Id AS [AccountId],
               a.Email,
               c.CountryCode,
               COUNT(*) AS [Trips],
               DENSE_RANK() OVER(PARTITION BY c.CountryCode ORDER BY COUNT(*) DESC, a.Id) AS [Rank]
          FROM Accounts a
    INNER JOIN AccountsTrips ats
            ON ats.AccountId = a.Id
    INNER JOIN Trips t
            ON t.Id = ats.TripId
    INNER JOIN Rooms r
            ON r.Id = t.RoomId
    INNER JOIN Hotels h
            ON h.Id = r.HotelId
    INNER JOIN Cities c
            ON c.Id = h.CityId
      GROUP BY c.CountryCode,
               a.Email,
               a.Id
)

  SELECT cte.AccountId,
         cte.Email,
         cte.CountryCode,
         cte.Trips
    FROM cte_TopTravelers cte
   WHERE cte.[Rank] = 1
ORDER BY cte.Trips DESC,
         cte.AccountId