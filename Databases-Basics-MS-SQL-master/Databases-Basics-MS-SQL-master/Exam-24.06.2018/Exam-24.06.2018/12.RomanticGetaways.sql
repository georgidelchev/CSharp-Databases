    SELECT a.Id,
           a.Email,
           c.Name AS [Town],
           COUNT(c.Name) AS [Trips]
      FROM Accounts a
INNER JOIN Cities c
        ON c.Id = a.CityId
INNER JOIN AccountsTrips ats
        ON ats.AccountId = a.Id
INNER JOIN Trips t
        ON T.Id = ats.TripId
INNER JOIN Rooms r
        ON r.Id = T.RoomId
INNER JOIN Hotels h
        ON h.Id = r.HotelId
     WHERE a.CityId = h.CityId
  GROUP BY a.Id,
           a.Email,
           c.Name
  ORDER BY [Trips] DESC,
           a.Id