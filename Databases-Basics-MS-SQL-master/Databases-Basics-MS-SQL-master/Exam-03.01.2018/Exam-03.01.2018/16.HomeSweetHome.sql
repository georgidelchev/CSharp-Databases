WITH cte_AllCars 
AS
(
        SELECT v.Id,
               o.ReturnOfficeId,
               v.OfficeId,
               m.Manufacturer,
               m.Model,
               DENSE_RANK() OVER (PARTITION BY v.Id ORDER BY o.CollectionDate DESC) AS [Rank]
          FROM Vehicles AS v
    INNER JOIN Models m 
            ON m.Id = v.ModelId
     LEFT JOIN Orders o 
            ON o.VehicleId = v.Id
)

  SELECT CONCAT(cte.Manufacturer, ' - ',  cte.Model) AS [Vehicle],
         CASE
             WHEN (SELECT COUNT(*) FROM Orders WHERE VehicleId = cte.Id) = 0 OR cte.OfficeId = cte.ReturnOfficeId THEN 'home'
             WHEN cte.ReturnOfficeId IS NULL THEN 'on a rent'
             WHEN cte.OfficeId <> cte.ReturnOfficeId THEN
                  (    SELECT CONCAT(t.[Name], ' - ', o.[Name])
                         FROM Offices AS o
                   INNER JOIN Towns AS t ON t.Id = o.TownId
                        WHERE cte.ReturnOfficeId = o.Id)
         END AS [Location]
    FROM cte_AllCars cte
   WHERE cte.[Rank] = 1
ORDER BY Vehicle, 
         cte.Id