    SELECT t.Name AS [Town Name],
           o.Name AS [Office Name],
           o.ParkingPlaces
      FROM Offices o
INNER JOIN Towns t
        ON t.Id = o.TownId
     WHERE o.ParkingPlaces > 25
  ORDER BY [Town Name],
           o.Id