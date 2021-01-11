SELECT e.EmployeeID, e.FirstName, p.Name,
       IIF(YEAR(p.StartDate) >= 2005, NULL, p.Name) AS ProjectName
    FROM Employees AS e
             INNER JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
             INNER JOIN Projects AS p ON ep.ProjectID = p.ProjectID
    WHERE e.EmployeeID = 24

