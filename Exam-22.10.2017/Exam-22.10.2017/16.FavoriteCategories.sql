  SELECT st.[Department Name],
         st.[Category Name],
         st.[Percentage]
    FROM (    SELECT d.Name AS [Department Name],
                     c.Name AS [Category Name],
                     CAST(ROUND(COUNT(1) OVER(PARTITION BY c.Id) * 100.0 / COUNT(1) OVER(PARTITION BY d.id), 0) AS INT) AS [Percentage]
                FROM Departments d
          INNER JOIN Categories c
                  ON c.DepartmentId = d.Id
          INNER JOIN Reports r
                  ON r.CategoryId = c.Id) AS st
GROUP BY st.[Department Name],
         st.[Category Name],
         st.[Percentage]
ORDER BY st.[Department Name],
         st.[Category Name],
         st.[Percentage]