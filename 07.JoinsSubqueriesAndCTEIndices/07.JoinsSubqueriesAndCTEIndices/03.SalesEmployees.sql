    SELECT e.EmployeeID,
           e.FirstName,
           e.LastName,
           d.Name AS [DepartmentName]    
      FROM Employees e
INNER JOIN Departments d
        ON d.DepartmentID = e.DepartmentID
     WHERE d.Name = 'Sales'
  ORDER BY e.EmployeeID