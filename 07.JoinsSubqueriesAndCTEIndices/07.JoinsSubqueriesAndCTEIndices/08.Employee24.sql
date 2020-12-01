    SELECT e.EmployeeID,
           e.FirstName,
           CASE
               WHEN p.StartDate >= '01-01-2005' THEN NULL
               ELSE p.Name
           END AS [ProjectName]
      FROM Employees e
INNER JOIN EmployeesProjects ep
        ON ep.EmployeeID = e.EmployeeID
INNER JOIN Projects p
        ON p.ProjectID = ep.ProjectID
     WHERE e.EmployeeID = 24