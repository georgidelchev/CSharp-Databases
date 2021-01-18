SELECT TOP 50 e.EmployeeID, CONCAT(e.FirstName, ' ', e.LastName),
       CONCAT(m.FirstName, ' ', m.LastName), d.Name
    FROM Employees AS e
             INNER JOIN Employees AS m ON m.EmployeeID = e.ManagerID
             INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    ORDER BY EmployeeID
