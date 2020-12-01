SELECT TOP 50
           e.EmployeeID,
           e.FirstName + ' ' + e.LastName AS [EmployeeName],
           m.FirstName + ' ' + m.LastName AS [ManagerName],
           d.Name AS [DepartmentName]
      FROM Employees e
INNER JOIN Employees m
        ON m.EmployeeID = e.ManagerID
INNER JOIN Departments d
        ON d.DepartmentID = e.DepartmentID
  ORDER BY e.EmployeeID