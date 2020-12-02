    SELECT m.Model,
           m.Seats,
           v.Mileage
      FROM Vehicles v
INNER JOIN Models m
        ON m.Id = v.ModelId
     WHERE v.Id <> ALL(SELECT o.VehicleId 
                         FROM Orders o 
                        WHERE o.ReturnDate IS NULL)
  ORDER BY v.Mileage,
           m.Seats DESC,
           m.Id