    SELECT d.Name AS [Department Name],
           ISNULL(CAST(AVG(DATEDIFF(DAY, r.OpenDate, r.CloseDate)) AS VARCHAR), 'no info') AS [Average Duration]
      FROM Departments d
 INNER JOIN Categories c
        ON c.DepartmentId = d.Id
INNER JOIN Reports r
        ON r.CategoryId = c.Id
  GROUP BY d.Name
  ORDER BY d.Name