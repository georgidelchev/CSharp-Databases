WITH cte_AccountsToCity 
AS
(
        SELECT ht.Id AS [HotelTownId],
               c.Name AS [HotelTown]
          FROM Cities c
    INNER JOIN Hotels ht
            ON ht.CityId = c.Id
)

    SELECT T.Id,
           a.FirstName + ' ' + ISNULL(a.MiddleName + ' ', '') + a.LastName AS [FullName],
           c.Name AS [From],
           cte.HotelTown AS [To],
           CASE
               WHEN t.CancelDate IS NULL THEN CAST(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate) AS NVARCHAR) + ' days'
               ELSE 'Canceled'
           END AS [Duration]
      FROM Trips t
INNER JOIN AccountsTrips ats
        ON ats.TripId = T.Id
 LEFT JOIN Accounts a
        ON a.Id = ats.AccountId
 LEFT JOIN Cities c
        ON c.Id = a.CityId
 LEFT JOIN Rooms r
        ON r.Id = T.RoomId
 LEFT JOIN cte_AccountsToCity cte
        ON cte.HotelTownId = r.HotelId
  ORDER BY [FullName],
           ats.TripId