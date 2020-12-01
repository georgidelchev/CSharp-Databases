WITH cte_MostOrdered
AS
(
SELECT TOP 7
           m.Model,
           AVG(m.Consumption) AS [AverageConsumption]
      FROM Models m
INNER JOIN Vehicles v
        ON v.ModelId = m.Id
INNER JOIN Orders o
        ON o.VehicleId = v.Id
  GROUP BY m.Model
  ORDER BY COUNT(o.Id) DESC
)

    SELECT m.Manufacturer,
           cte.AverageConsumption
      FROM cte_MostOrdered cte
INNER JOIN Models m
        ON m.Model = cte.Model
     WHERE m.Consumption BETWEEN 5 AND 15
  ORDER BY m.Manufacturer,
           cte.AverageConsumption