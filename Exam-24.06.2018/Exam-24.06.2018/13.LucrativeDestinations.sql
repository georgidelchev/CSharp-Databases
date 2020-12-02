SELECT TOP 10
           c.Id,
           c.Name,
           SUM(h.BaseRate + r.Price) AS [Total Revenue],
           COUNT(t.Id) AS [Trips]
      FROM Cities c
INNER JOIN Hotels h
        ON h.CityId = c.Id
INNER JOIN Rooms r
        ON r.HotelId = h.Id
INNER JOIN Trips t
        ON t.RoomId = r.Id
     WHERE DATEPART(YEAR, t.BookDate) = 2016
  GROUP BY c.Id,
           c.Name
  ORDER BY [Total Revenue] DESC,
           [Trips] DESC