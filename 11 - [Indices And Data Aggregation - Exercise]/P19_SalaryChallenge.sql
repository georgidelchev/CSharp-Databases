SELECT TOP 10 e.FirstName,
              e.LastName,
              e.DepartmentID
    FROM Employees AS e
    WHERE e.Salary > (SELECT AVG(e1.Salary)
                          FROM Employees AS e1
                          WHERE e.DepartmentID = e1.DepartmentID)
    ORDER BY e.DepartmentID
