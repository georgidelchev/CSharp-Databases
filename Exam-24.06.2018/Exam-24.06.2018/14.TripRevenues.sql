    SELECT ats.TripId,
           h.Name AS [HotelName],
           r.Type AS [RoomType],
           CASE
               WHEN t.CancelDate IS NULL THEN SUM(h.BaseRate + r.Price)
               ELSE 0
           END AS [Revenue]
      FROM Trips t
INNER JOIN Rooms r
        ON r.Id = t.RoomId
INNER JOIN Hotels h
        ON h.Id = r.HotelId
INNER JOIN AccountsTrips ats
        ON ats.TripId = t.Id
  GROUP BY ats.TripId,
           h.Name,
           r.Type,
           t.CancelDate
  ORDER BY r.Type,
           ats.TripId