    SELECT c.Name AS [CategoryName],
           COUNT(e.Id) AS [Employees Number]
      FROM Categories c
INNER JOIN Departments d
        ON d.Id = c.DepartmentId
INNER JOIN Employees e
        ON e.DepartmentId = d.Id
  GROUP BY c.Name
  ORDER BY c.Name