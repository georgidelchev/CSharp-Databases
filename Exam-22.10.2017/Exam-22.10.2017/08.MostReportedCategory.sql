    SELECT c.Name AS [CategoryName],
           COUNT(r.Id) AS [ReportsNumber]
      FROM Categories c
INNER JOIN Reports r
        ON r.CategoryId = c.Id
  GROUP BY c.Name
  ORDER BY [ReportsNumber] DESC,
           c.Name