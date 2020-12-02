   SELECT m.Manufacturer,
          m.Model,
          COUNT(o.Id) AS [TimesOrdered]
     FROM Vehicles v
LEFT JOIN Orders o
       ON o.VehicleId = v.Id
LEFT JOIN Models m
       ON m.Id = v.ModelId
 GROUP BY m.Manufacturer,
          m.Model
 ORDER BY [TimesOrdered] DESC,
          m.Manufacturer DESC,
          m.Model