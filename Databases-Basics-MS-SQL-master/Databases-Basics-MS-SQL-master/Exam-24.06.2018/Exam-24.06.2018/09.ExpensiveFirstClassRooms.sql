    SELECT r.Id,
           r.Price,
           h.Name AS [Hotel],
           c.Name AS [City]
      FROM Hotels h
INNER JOIN Cities c
        ON c.Id = h.CityId
INNER JOIN Rooms r
        ON r.HotelId = h.Id
     WHERE r.Type = 'First Class'
  ORDER BY r.Price DESC,
           r.Id