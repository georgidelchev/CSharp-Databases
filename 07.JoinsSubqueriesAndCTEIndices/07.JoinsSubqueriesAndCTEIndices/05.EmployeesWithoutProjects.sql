SELECT TOP 3
           e.EmployeeID,
           e.FirstName
      FROM Employees e
 LEFT JOIN EmployeesProjects ep
        ON ep.EmployeeID = e.EmployeeID
 LEFT JOIN Projects p
        ON p.ProjectID = ep.ProjectID
     WHERE p.ProjectID IS NULL
  ORDER BY e.EmployeeID