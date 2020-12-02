    SELECT e.EmployeeID,
           e.FirstName,
           e.ManagerID,
           m.FirstName AS [ManagerName]
      FROM Employees e
INNER JOIN Employees m
        ON m.EmployeeID = e.ManagerID
     WHERE e.ManagerID IN (3, 7)
  ORDER BY e.EmployeeID