WITH cte_MostFriquentClass
AS
(
    SELECT c.FirstName + ' ' + c.LastName AS [Name],
           m.Class,
           DENSE_RANK() OVER(PARTITION BY c.FirstName, c.LastName ORDER BY COUNT(m.Class) DESC) AS [Rank]
      FROM Clients c
INNER JOIN Orders o
        ON o.ClientId = c.Id
INNER JOIN Vehicles v
        ON v.Id = o.VehicleId
INNER JOIN Models m
        ON m.Id = v.ModelId
  GROUP BY c.FirstName, 
           c.LastName, 
           m.Class
)

SELECT cte.Name,
       cte.Class
  FROM cte_MostFriquentClass cte
 WHERE cte.[Rank] = 1